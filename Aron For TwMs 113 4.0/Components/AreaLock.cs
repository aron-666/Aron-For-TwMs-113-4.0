using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessTools;

namespace Aron_For_TwMs_113_4.Components
{
    public partial class AreaLock : UserControl
    {
        private readonly IComponentEvents _componentEvents;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        
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
            radAutoLock.Checked = s.IsAutoLock;
            radSelect.Checked = !s.IsAutoLock;
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
                if (!MapleProcess.IsOpen && radAutoLock.Checked)
                {
                    Process[] ps = Process.GetProcesses();
                    var p = ps.Where(x => x.ProcessName.Contains(textBox1.Text)).FirstOrDefault();
                    if (p == null)
                    {
                        _componentEvents.SetStatusText("未鎖定");
                        return;
                    }

                    if (MapleProcess.LoadFromPid(p.Id))
                    {
                        _componentEvents.SetStatusText($"已鎖定 {MapleProcess.Pid}");
                        StringBuilder ClassName = new StringBuilder(256);
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


                }
                else
                {

                }

                if (MapleProcess.IsOpen)
                {
                    StringBuilder ClassName = new StringBuilder(256);
                    GetClassName(MapleProcess.MsProc.MainWindowHandle, ClassName, ClassName.Capacity);
                    if (ClassName.ToString() == "StartUpDlgClass")
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

        public static ProcessInfo Notting => new ProcessInfo() { Name = "Notting", Id = -1 };
    }
}
