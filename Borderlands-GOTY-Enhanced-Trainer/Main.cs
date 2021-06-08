using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Memory;

namespace Borderlands_GOTY_Enhanced_Trainer
{
    public partial class Main : Form
    {
        ///Components
        #region

        //Memory.dll
        Mem m = new Mem();
        private const string BORDERLANDSPROCESS = "BorderlandsGOTY.exe";

        //Hotkey Usage
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        //Offsets
        public static class Offsets
        {
            public const string Money = "0x025C1D90,0x68,0x550,0x1F8,0x350";                        //Int
            public const string GoldenKeyTotal = "0x025C1D90,0x68,0x550,0x1B8,0x1070,0x64,0x300";   //Int
            public const string SkillPoints = "0x025C1D90,0x68,0x550,0x1F8,0x348";                  //Int
            public const string GoldenKeyUsed = "0x025C1D90,0x68,0x550,0x1B8,0x1070,0x64,0x31C";    //Int
            public const string SkillCooldown = "0x025C1D90,0x68,0x550,0x1F8,0x36C,0x270,0x98";     //Float
            public const string Level = "0x025C1D90,0x68,0x550,0x1F8,0x32C";                        //Int
            public const string Experience = "0x025C1D90,0x68,0x550,0x1F8,0x358,0x268,0x98";        //Float
            public const string Health = "0x025C1D90,0x68,0x550,0x1F8,0x364,0x98";                  //Float
            public const string MaxHealth = "0x025C1D90,0x68,0x550,0x1F8,0x364,0x80";               //Float
            public const string Shield = "0x025C1D90,0x68,0x550,0x390,0x290,0x98";                  //Float
            public const string MaxShield = "0x025C1D90,0x68,0x550,0x390,0x290,0x80";               //Float
        }
        #endregion

        ///Main Form
        #region

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int PID = m.GetProcIdFromName(BORDERLANDSPROCESS);
            if (PID > 0)
            {
                m.OpenProcess(PID);
                //MessageBox.Show("Connected to Game");
            }
        }
        #endregion

        ///Timers
        #region

        // Memory Reader
        private void ProcessTimer_Tick(object sender, EventArgs e)
        {
            //Memory.dll x64
            int PID = m.GetProcIdFromName(BORDERLANDSPROCESS);
            if (PID > 0)
            {
                m.OpenProcess(PID);
                CurrentLvLabel.Text = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.Level}").ToString();
                MoneyLabel.Text = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.Money}").ToString();
                HealthLabel.Text = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.Health}").ToString();
                ShieldLabel.Text = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.Shield}").ToString();
                XPLabel.Text = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.Experience}").ToString();
                SkillPointsLabel.Text = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.SkillPoints}").ToString();
                GoldenKeysLabel.Text = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.GoldenKeyTotal}").ToString();
            }
        }

        //No Clip Timer
        private void NoClipTimer_Tick(object sender, EventArgs e)
        {

        }

        //Replenish Health and Shields (NUMPAD 0)
        private void HotKeyTimer_Tick(object sender, EventArgs e)
        {
            var maxValueHealth = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.MaxHealth}");
            var maxValueShields = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.MaxShield}");

            if (GetAsyncKeyState(Keys.NumPad0) < 0)
            {
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.Health}", "float", maxValueHealth.ToString());
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.Shield}", "float", maxValueShields.ToString());
            }
        }


        #endregion

        ///Buttons
        #region

        private void MoneyButton_Click(object sender, EventArgs e)
        {
            if (MoneyTextBox.Text != "")
            {
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.Money}", "int", MoneyTextBox.Text);
            }
        }

        private void XPButton_Click(object sender, EventArgs e)
        {
            if (XPTextBox.Text != "")
            {
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.Level}", "int", XPTextBox.Text);
            }
        }

        private void KeysButton_Click(object sender, EventArgs e)
        {
            if (KeysTextBox.Text != "")
            {
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.GoldenKeyTotal}", "int", KeysTextBox.Text);
            }
        }

        #endregion

        ///Checkboxes
        #region

        //Freeze Everything .... why not ... feel free to adjust this lol
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //Health
                var oldHealthValue = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.Health}");
                var newHealthValue = oldHealthValue;
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Health}", "float", newHealthValue.ToString());

                //Shields
                var oldShieldValue = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.Shield}");
                var newShieldValue = oldShieldValue;
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Shield}", "float", newShieldValue.ToString());

                //Current Level
                var oldLevelValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.Level}");
                var newLevelValue = oldLevelValue;
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Level}", "int", newLevelValue.ToString());

                //Money
                var oldMoneyValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.Money}");
                var newMoneyValue = oldMoneyValue;
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Money}", "int", newMoneyValue.ToString());

                //Experience
                var oldXPValue = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.Experience}");
                var newXPValue = oldXPValue;
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Experience}", "float", newXPValue.ToString());

                //SkillPoints
                var oldSkillPointsValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.SkillPoints}");
                var newSkillPointsValue = oldSkillPointsValue;
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.SkillPoints}", "int", newSkillPointsValue.ToString());

                //Golden Keys
                var oldKeysValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.GoldenKeyTotal}");
                var newKeysValue = oldKeysValue;
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.GoldenKeyTotal}", "int", newKeysValue.ToString());
            }
            else //Unfreeze the values on uncheck event
            {
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Health}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Shield}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Level}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Money}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Experience}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.SkillPoints}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.GoldenKeyTotal}");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                //Health
                var oldHealthValue = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.MaxHealth}");
                var newHealthValue = oldHealthValue;
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Health}", "float", newHealthValue.ToString());

                //Shields
                var oldShieldValue = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.MaxShield}");
                var newShieldValue = oldShieldValue;
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Shield}", "float", newShieldValue.ToString());
            }
            else
            {
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Health}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Shield}");
            }
        }
        #endregion

    }
}
