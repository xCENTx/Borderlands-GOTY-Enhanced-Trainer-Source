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
            //Player Info
            public const string Money = "0x025C1D90,0x68,0x550,0x1F8,0x350";                                //Int
            public const string GoldenKeyTotal = "0x025C1D90,0x68,0x550,0x1B8,0x1070,0x64,0x300";           //Int
            public const string SkillPoints = "0x025C1D90,0x68,0x550,0x1F8,0x348";                          //Int
            public const string GoldenKeyUsed = "0x025C1D90,0x68,0x550,0x1B8,0x1070,0x64,0x31C";            //Int
            public const string SkillCooldown = "0x025C1D90,0x68,0x550,0x1F8,0x36C,0x270,0x98";             //Float
            public const string Level = "0x025C1D90,0x68,0x550,0x1F8,0x32C";                                //Int
            public const string Experience = "0x025C1D90,0x68,0x550,0x1F8,0x358,0x268,0x98";                //Float
            public const string Health = "0x025C1D90,0x68,0x550,0x1F8,0x364,0x98";                          //Float
            public const string MaxHealth = "0x025C1D90,0x68,0x550,0x1F8,0x364,0x80";                       //Float
            public const string Shield = "0x025C1D90,0x68,0x550,0x390,0x290,0x98";                          //Float
            public const string MaxShield = "0x025C1D90,0x68,0x550,0x390,0x290,0x80";                       //Float
            public const string SkillCooldownTimerMax = "0x025C1D90,0x68,0x550,0x1F8,0x36C,0x270,0x80";     //Float
            public const string XPMultiplier = "0x025C1D90,0x68,0x550,0x1F8,0x358,0x268,0x194";             //Float
            public const string MaxXP = "0x025C1D90,0x68,0x550,0x1F8,0x358,0x268,0x80";                     //Float

            //Player Position (Working so far)
            public const string CameraPitch = "0x02555AA8,0xC4,0x630,0x10,0x9C";            //Int
            public const string CameraYaw = "0x02555AA8,0xC4,0x630,0x10,0xA0";              //Int
            public const string PlayerX = "0x025C1DA0,0x18,0x40,0x90";                      //Float
            public const string PlayerY = "0x025C1DA0,0x18,0x40,0x94";                      //Float
            public const string PlayerZ = "0x025C1DA0,0x18,0x40,0x98";                      //Float

            //Player Ammo
            public const string RevolverAmmo = "0x02542680,98";                             //Float
            public const string RevolverAmmoMax = "0x02542680,80";                          //Float
            public const string SMGAmmo = "0x02542678,98";                                  //Float
            public const string SMGAmmoMax = "0x02542678,80";		                        //Float
            public const string CarbineAmmo = "0x02542690,0x98";                            //Float
            public const string CarbineAmmoMax = "0x02542690,0x80";		                    //Float
            public const string ShotgunShells = "0x02542670,98";                            //Float
            public const string ShotgunShellsMax = "0x02542670,80";	                        //Float
            public const string SniperRifleAmmo = "0x02542668,98";	                        //Float
            public const string SniperRifleAmmoMax = "0x02542668,80";                       //Float
            public const string Grenades = "0x025C1D90,0x50,0x280,0x98";					//Float
            public const string GrenadesMax = "0x025C1D90,0x50,0x280,0x80";                 //Float
            public const string RepeaterPistolAmmo = "0x025C1D90,0x50,0x2A0,0x98";		    //Float
            public const string RepeaterPistolMax = "0x025C1D90,0x50,0x2A0,0x80";           //Float
            public const string LauncherAmmo = "0x02542660,0x98";                           //Float
            public const string LauncherAmmoMax = "0x02542660,0x80";				        //Float
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
            var PlayerXPos = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.PlayerX}");
            var PlayerYPos = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.PlayerY}");
            var PlayerZPos = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.PlayerZ}");

            //Fly (Working ... Disabled for now)
            if (GetAsyncKeyState(Keys.Space) < 0)
            {
                //var newValue = PlayerZPos + 50;
                //m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.PlayerZ}", "float", newValue.ToString());
            }
        }

        //Numpad Functions
        private void HotKeyTimer_Tick(object sender, EventArgs e)
        {
            //Health and Shields
            var maxValueHealth = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.MaxHealth}");
            var maxValueShields = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.MaxShield}");

            //Ammo Types
            var maxValueGrenades = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.GrenadesMax}");
            var maxValueRepeater = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.RepeaterPistolMax}");
            var maxValueRevolver = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.RevolverAmmoMax}");
            var maxValueSMG = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.SMGAmmoMax}");
            var maxValueShells = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.ShotgunShellsMax}");
            var maxValueSniper = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.SniperRifleAmmoMax}");
            var maxValueLauncher = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.LauncherAmmoMax}");

            //Refill Health, Shields and Ammo (NUMPAD 0)
            if (GetAsyncKeyState(Keys.NumPad0) < 0)
            {
                //Refill Health and Shields
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.Health}", "float", maxValueHealth.ToString());
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.Shield}", "float", maxValueShields.ToString());

                //Refill All Ammo Types
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.Grenades}", "float", maxValueGrenades.ToString());
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.RepeaterPistolAmmo}", "float", maxValueRepeater.ToString());
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.RevolverAmmo}", "float", maxValueRevolver.ToString());
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.SMGAmmo}", "float", maxValueSMG.ToString());
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.ShotgunShells}", "float", maxValueShells.ToString());
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.SniperRifleAmmo}", "float", maxValueSniper.ToString());
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.LauncherAmmo}", "float", maxValueLauncher.ToString());
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

                //Golden Keys Used
                var oldUsedKeysValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.GoldenKeyUsed}");
                var newUsekdKeysValue = oldUsedKeysValue;
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.GoldenKeyUsed}", "int", newUsekdKeysValue.ToString());

                //Skill Cooldown Timer
                var oldCooldownValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.SkillCooldown}");
                var newCooldownValue = oldCooldownValue;
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.SkillCooldown}", "int", newCooldownValue.ToString());

            }
            else //Unfreeze the values on uncheck event
            {
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Health}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Shield}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Level}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Money}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Experience}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.SkillPoints}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.SkillCooldown}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.GoldenKeyTotal}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.GoldenKeyUsed}");
            }
        }

        //Unlimited Health , Shields and Ammo
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

                //Ammo
                var maxValueGrenades = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.GrenadesMax}");
                var maxValueRepeater = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.RepeaterPistolMax}");
                var maxValueRevolver = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.RevolverAmmoMax}");
                var maxValueSMG = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.SMGAmmoMax}");
                var maxValueShells = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.ShotgunShellsMax}");
                var maxValueSniper = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.SniperRifleAmmoMax}");
                var maxValueLauncher = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.LauncherAmmoMax}");
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Grenades}", "float", maxValueGrenades.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.RepeaterPistolAmmo}", "float", maxValueRepeater.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.RevolverAmmo}", "float", maxValueRevolver.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.SMGAmmo}", "float", maxValueSMG.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.ShotgunShells}", "float", maxValueShells.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.SniperRifleAmmo}", "float", maxValueSniper.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.LauncherAmmo}", "float", maxValueLauncher.ToString());

            }
            else
            {
                //Health and Shields
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Health}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Shield}");

                //Ammo
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Grenades}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.RepeaterPistolAmmo}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.RevolverAmmo}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.SMGAmmo}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.ShotgunShells}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.SniperRifleAmmo}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.LauncherAmmo}");
            }
        }
        #endregion
    }
}
