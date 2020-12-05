using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ProcessTools;

namespace Aron_For_TwMs_113_4.Components
{
    public partial class AreaLock : UserControl
    {
        private readonly IComponentEvents _componentEvents;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        private Process DxWnd;
        public AreaLock(IComponentEvents componentEvents)
        {
            InitializeComponent();
            _componentEvents = componentEvents;
        }

        private void AreaLock_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            var s = Properties.Settings.Default;
            textBox1.DataBindings.Add(new Binding("Text", s, "Lock_Filter"));
            LoadProcessList(textBox1.Text);
            ckAutoSave.DataBindings.Add(new Binding("Checked", s, "IsAutoSave"));
            CkStart.DataBindings.Add(new Binding("Checked", s, "IsAutoPlay"));
            CkBypass.DataBindings.Add(new Binding("Checked", s, "IsAutoBypass"));
            ckNotify.DataBindings.Add(new Binding("Checked", s, "IsNotify"));
            ckDwWnd.DataBindings.Add(new Binding("Checked", s, "IsDxWnd"));
            radAutoLock.Checked = s.IsAutoLock;
            radSelect.Checked = !s.IsAutoLock;


            openFileDialog1.FileName = System.IO.Path.GetFullPath(s.ExePath);
            openFileDialog1_FileOk(null, null);

            comboBox2.SelectedIndexChanged += combo2_autoApply;
            reSetServers();

        }

        private void combo2_autoApply(object sender, EventArgs e)
        {
            _componentEvents.StartResualt = comboBox2.SelectedItem as Models.Data;
        }

        private List<Models.Data> GetServers()
        {
            
            WebClient client = new MyWebClient();
            List<Models.Data> ret = null;
            try
            {
                var url = new Uri(Properties.Resources.SiteRoot + Properties.Resources.StarterUrl);
                var res = client.DownloadString(url);
                
                    
                var data = JsonConvert.DeserializeObject<Models.ApiResualt<List<Models.Data>>>(res);
                ret = data.data;
                
            }
            catch
            {
                ret = new List<Models.Data>();
            }
            var first = JsonConvert.DeserializeObject<Models.Data>(Properties.Settings.Default.UserStarter);
            ret.Add(first);
            ret.Reverse();
            return ret;
        }
        private void LoadProcessList(string filter = null)
        {
            Process[] ps = Process.GetProcesses();

            List<ProcessInfo> procList = filter == null ?
                ps.OrderBy(x => x.ProcessName)
                    .Select(x => new ProcessInfo() { Name = x.ProcessName, Id = x.Id })
                    .ToList() :
                ps.Where(x => x.ProcessName.Contains(filter))
                    .OrderBy(x => x.ProcessName)
                    .Select(x => new ProcessInfo() { Name = x.ProcessName, Id = x.Id })
                    .ToList();

            foreach (var i in ps)
            {
                i.Close();
                i.Dispose();
            }

            procList.Reverse();
            procList.Add(ProcessInfo.Notting);
            procList.Reverse();
            comboBox1.DataSource = procList;

        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            LoadProcessList(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!radSelect.Checked)
                return;

            var item = comboBox1.SelectedItem as ProcessInfo;
            if (item.IsNotting())
            {
                MessageBox.Show("請選擇程式", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (MapleProcess.LoadFromPid(item.Id))
                {
                    _componentEvents.SetStatusText($"已鎖定 {item.Id}");
                    MessageBox.Show("鎖定成功!", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StringBuilder ClassName = new StringBuilder(256);
                    MapleProcess.MsProc.Refresh();
                    GetClassName(MapleProcess.MsProc.MainWindowHandle, ClassName, ClassName.Capacity);
                    if (ClassName.ToString() == "StartUpDlgClass")
                    {
                        //自動注入bypass
                        if (CkBypass.Enabled)
                        {
                            _componentEvents.GetCt()["bypass"].CeAutoAsm(true);
                        }
                        if (CkStart.Checked)
                        {
                            Task.Factory.StartNew(() =>
                            {
                                Thread.Sleep(1000);
                                MapleProcess.MsProc.CloseMainWindow();
                            });

                        }
                    }
                }
                else
                {
                    MessageBox.Show("鎖定失敗!", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAutoSave.Checked)
            {
                Properties.Settings.Default.IsAutoLock = radAutoLock.Checked;
                Properties.Settings.Default.Save();
            }
            if (radAutoLock.Checked)
            {
                button1.Enabled = false;
                comboBox1.Enabled = false;
                comboBox1.SelectedIndex = 0;
                //t_Lock.Enabled = true;
            }
            else
            {
                //t_Lock.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAutoSave.Checked)
            {
                Properties.Settings.Default.IsAutoLock = radAutoLock.Checked;
                Properties.Settings.Default.Save();
            }
            if (radSelect.Checked)
            {
                button1.Enabled = true;
                comboBox1.Enabled = true;
                //t_Lock.Enabled = false;
            }
            else
            {

            }
        }

        private void t_Lock_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MapleProcess.IsOpen && radAutoLock.Checked)
                {
                    _componentEvents.SetStatusText($"已鎖定 {MapleProcess.Pid}");
                }
                if (!MapleProcess.IsOpen && radAutoLock.Checked)
                {

                    Process[] ps = Process.GetProcesses();
                    var p = ps.Where(x => x.ProcessName.Contains(textBox1.Text)).FirstOrDefault();
                    if (p == null)
                    {
                        _componentEvents.SetStatusText("未鎖定");
                        foreach (var i in ps) i.Dispose();
                        return;
                    }

                    if (MapleProcess.LoadFromPid(p.Id))
                    {
                        StringBuilder ClassName = new StringBuilder(256);
                        MapleProcess.MsProc.Refresh();
                        GetClassName(MapleProcess.MsProc.MainWindowHandle, ClassName, ClassName.Capacity);
                        if (ClassName.ToString() == "StartUpDlgClass")
                        {
                            //自動注入bypass
                            if (CkBypass.Enabled)
                            {
                                _componentEvents.GetCt()["bypass"].CeAutoAsm(true);
                            }
                            if (CkStart.Checked)
                            {
                                Task.Factory.StartNew(() =>
                                {
                                    Thread.Sleep(1000);
                                    MapleProcess.MsProc.CloseMainWindow();
                                });

                            }
                        }
                    }
                    foreach (var i in ps) i.Dispose();

                    
                }
                else
                {
                }

                if (MapleProcess.IsOpen)
                {
                    StringBuilder ClassName = new StringBuilder(256);
                    MapleProcess.MsProc.Refresh();
                    GetClassName(MapleProcess.MsProc.MainWindowHandle, ClassName, ClassName.Capacity);
                    var className = ClassName.ToString();
                    if (className == "StartUpDlgClass")
                    {
                        if (CkStart.Checked)
                        {
                            Task.Factory.StartNew(() =>
                            {
                                Thread.Sleep(1000);
                                MapleProcess.MsProc.CloseMainWindow();
                            });

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                
            }
            
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ckAutoSave.Checked)
            {
                Properties.Settings.Default.Lock_Filter = textBox1.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Bubypass_Click(object sender, EventArgs e)
        {
            _componentEvents.GetCt()["bypass"].CeAutoAsm(true);
        }

        private void S_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsAutoSave = ckAutoSave.Checked;
            Properties.Settings.Default.Save();
        }

        private void CkBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAutoSave.Checked)
            {
                Properties.Settings.Default.IsAutoBypass = CkBypass.Checked;
                Properties.Settings.Default.Save();
            }
        }

        private void CkStart_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAutoSave.Checked)
            {
                Properties.Settings.Default.IsAutoPlay = CkStart.Checked;
                Properties.Settings.Default.Save();
            }
        }

        private void ckNotify_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAutoSave.Checked)
            {
                Properties.Settings.Default.IsNotify = ckNotify.Checked;
                Properties.Settings.Default.Save();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            lbExeFile.Text = openFileDialog1.FileName;
            //儲存到設定
            if (Path.GetFullPath(Properties.Settings.Default.ExePath) != Path.GetFullPath(openFileDialog1.FileName))
            {
                if (ckAutoSave.Checked)
                {
                    Properties.Settings.Default.ExePath = openFileDialog1.FileName;
                    Properties.Settings.Default.Save();
                }
            }
            

        }

        private void reSetServers()
        {
            var t = GetServers();
            comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
            comboBox2.DataSource = t;
            if(comboBox2.Items.Count >= Properties.Settings.Default.ServerIndex + 1) 
                comboBox2.SelectedIndex = Properties.Settings.Default.ServerIndex;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        }
        private void btnUserSetting_Click(object sender, EventArgs e)
        {
            var setting = new StarterSetting();
            if(setting.ShowDialog() == DialogResult.OK)
            {
                reSetServers();
            }
        }

        private void btnF5_Click(object sender, EventArgs e)
        {
            reSetServers();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ckAutoSave.Checked)
            {
                Properties.Settings.Default.ServerIndex = comboBox2.SelectedIndex;
                Properties.Settings.Default.Save();
            }
        }

        private void lbExeFile_TextChanged(object sender, EventArgs e)
        {
            _componentEvents.ExePath = lbExeFile.Text;
            if (ckDwWnd.Checked)
            {
                //DxWnd.TARGETMAP[] args = new DxWnd.TARGETMAP[2]
                //{
                //    new DxWnd.TARGETMAP(){ path = lbExeFile.Text, dxversion = 8, flags = 0, initx = 0, inity = 0, minx = 0, miny = 0, maxx = 1920, maxy = 1080 },
                //    new DxWnd.TARGETMAP()
                //};
                //DxWnd.EndHook();
                //DxWnd.SetTarget(args);
                //DxWnd.StartHook();
                var id = Process.GetCurrentProcess().Id;
                ProcessStartInfo info = new ProcessStartInfo("DxWnd.exe")
                {
                    Arguments = $"{lbExeFile.Text} {id}"
                };

                

                if (info.Arguments.Length > 1 && info.Arguments[info.Arguments.Length - 1] == ' ') info.Arguments = info.Arguments.Remove(info.Arguments.Length - 1);
                DxWnd = Process.Start(info);
            }
            else
            {
                DxWnd?.Kill();
                DxWnd?.Dispose();
                DxWnd = null;
            }
        }

        private void ckDwWnd_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAutoSave.Checked)
            {
                Properties.Settings.Default.IsDxWnd = ckDwWnd.Checked;
                Properties.Settings.Default.Save();
            }
            if (ckDwWnd.Checked)
            {

                //DxWnd.TARGETMAP[] args = new DxWnd.TARGETMAP[2]
                //{
                //    new DxWnd.TARGETMAP(){ path = lbExeFile.Text, dxversion = 8, flags = 0, initx = 0, inity = 0, minx = 0, miny = 0, maxx = 1920, maxy = 1080 },
                //    new DxWnd.TARGETMAP()
                //};
                //DxWnd.EndHook();
                //DxWnd.SetTarget(args);
                //DxWnd.StartHook();

                var id = Process.GetCurrentProcess().Id;
                ProcessStartInfo info = new ProcessStartInfo("DxWnd.exe")
                {
                    Arguments = $"{lbExeFile.Text} {id}"
                };



                if (info.Arguments.Length > 1 && info.Arguments[info.Arguments.Length - 1] == ' ') info.Arguments = info.Arguments.Remove(info.Arguments.Length - 1);
                DxWnd = Process.Start(info);
            }
            else
            {
                DxWnd?.Kill();
                DxWnd?.Dispose();
                DxWnd = null;
            }
        }
    }

    public class ProcessInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public string NameAndId { get => $"{Id.ToString().PadRight(7)}{Name}"; }

        public override string ToString()
        {
            if (IsNotting())
                return Name;
            else
                return NameAndId;
        }

        public bool IsNotting()
        {
            return Id == -1;
        }

        public static ProcessInfo Notting => new ProcessInfo() { Name = "Nothing", Id = -1 };
    }

    public class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest WR = base.GetWebRequest(uri);
            WR.Timeout = 4 * 1000;
            return WR;
        }
    }
}
