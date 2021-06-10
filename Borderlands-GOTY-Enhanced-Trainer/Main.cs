using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Memory;

///---=>xCENTx<=---\\\
///---=>#8016<=---\\\

/// <-||-FUNCTIONS-||--------------------------------------<
/// // 
/// No Recoil
/// Inf Money
/// Inf Health
/// Inf Shields
/// Inf Ammo
/// Inf Golden Keys
/// Inf Skill Points
/// Max XP
/// Much, Much, More ... Check Out OFFSETS (Line 48)
/// //

/// <-||-CONTROLS-||-----------------------------------------<
/// //
/// NUMPAD 0 = REFILL HEALTH , SHIELDS & AMMO || (Line 188)
/// NUMPAD 1 = NO RECOIL ON || (Line 205)
/// NUMPAD 2 = NO RECOIL OFF || (Line 216)
/// NUMPAD 3 = ||BROKEN|| Toggle No Recoil On/Off || (Line 227)
/// //

namespace Borderlands_GOTY_Enhanced_Trainer
{
    public partial class Main : Form
    {
        ///Imports
        #region

        //Memory.dll
        Mem m = new Mem();
        private const string BORDERLANDSPROCESS = "BorderlandsGOTY.exe";

        //Hotkey Usage
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        #endregion

        ///Offsets
        #region

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

            //No Recoil Inject Attempt
            //Default Byte Array = F3 44 0F 59 A7 CC 0F 00 00 // mulss xmm12,[rdi+00000FCC]
            //New Byte Array = 45 0F 57 E4 90 90 90 90 90) // xorps xmm12,xmm12 => nop,nop,nop,nop,nop
            public const string NoRecoilInject = "0x1412B05"; //Bytes
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

        //Memory Reader
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

        //No Clip Timer || INACTIVE ||
        private void NoClipTimer_Tick(object sender, EventArgs e)
        {
            var PlayerXPos = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.PlayerX}");
            var PlayerYPos = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.PlayerY}");
            var PlayerZPos = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.PlayerZ}");

            ///Debug
            #region

            //Fly (Working ... Disabled for now)
            //if (GetAsyncKeyState(Keys.Space) < 0)
            //{
            //    var newValue = PlayerZPos + 20;
            //    m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.PlayerZ}", "float", newValue.ToString());
            //}

            //if (GetAsyncKeyState(Keys.LControlKey) < 0)
            //{
            //    var newValue = PlayerZPos - 20;
            //    m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.PlayerZ}", "float", newValue.ToString());
            //}

            //if (GetAsyncKeyState(Keys.Up) < 0)
            //{
            //    var newValue = PlayerXPos + 5;
            //    m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.PlayerZ}", "float", newValue.ToString());
            //}

            //if (GetAsyncKeyState(Keys.Down) < 0)
            //{
            //    var newValue = PlayerXPos - 5;
            //    m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.PlayerZ}", "float", newValue.ToString());
            //}

            //if (GetAsyncKeyState(Keys.Left) < 0)
            //{
            //    var newValue = PlayerYPos + 5;
            //    m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.PlayerZ}", "float", newValue.ToString());
            //}

            //if (GetAsyncKeyState(Keys.Right) < 0)
            //{
            //    var newValue = PlayerYPos - 5;
            //    m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.PlayerZ}", "float", newValue.ToString());
            //}
            #endregion
        }

        //Numpad Functions || 3/4 ACTIVE ||
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
            var maxValueCarbine = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.CarbineAmmoMax}");

            //Refill Health, Shields and Ammo (NUMPAD 0) || ACTIVE ||
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
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.CarbineAmmo}", "float", maxValueCarbine.ToString());
            }

            //No Recoil Mod (NUMPAD 1 Toggle ON) || ACTIVE ||
            if (GetAsyncKeyState(Keys.NumPad1) < 0)
            {
                ///Debug
                //On
                m.WriteMemory("BorderlandsGOTY.exe+1412B05", "bytes", "45 0F 57 E4 90 90 90 90 90");

                //Off
                //m.WriteMemory("BorderlandsGOTY.exe+1412B05", "bytes", "F3 44 0F 59 A7 CC 0F 00 00");
            }

            //No Recoil Mod (NUMPAD 2 Toggle OFF) || ACTIVE ||
            if (GetAsyncKeyState(Keys.NumPad2) < 0)
            {
                ///Debug
                //On
                //m.WriteMemory("BorderlandsGOTY.exe+1412B05", "bytes", "45 0F 57 E4 90 90 90 90 90");

                //Off
                m.WriteMemory("BorderlandsGOTY.exe+1412B05", "bytes", "F3 44 0F 59 A7 CC 0F 00 00");
            }

            //No Recoil Mod (NUMPAD 3 Toggle ON/OFF) || INACTIVE ||
            if (GetAsyncKeyState(Keys.NumPad3) < 0)
            {
                ///Debug
                #region
                //On
                //m.WriteMemory("BorderlandsGOTY.exe+1412B05", "bytes", "45 0F 57 E4 90 90 90 90 90");

                //Off
                //m.WriteMemory("BorderlandsGOTY.exe+1412B05", "bytes", "F3 44 0F 59 A7 CC 0F 00 00");
                //--------------------------------------------------------------------------------------

                //var ToggleStatus = m.ReadBytes("BorderlandsGOTY.exe+1412B05", 9);
                //var ON = new byte[] { 0x45, 0x0F, 0x57, 0xE4, 0x90, 0x90, 0x90, 0x90, 0x90 };
                //var OFF = new byte[] { 0xF3, 0x44, 0x0F, 0x59, 0xA7, 0xCC, 0x0F, 0x00, 0x00 };
                
                //if (m.ReadBytes("BorderlandsGOTY.exe+1412B05", 9) == new byte[] { 0xF3, 0x44, 0x0F, 0x59, 0xA7, 0xCC, 0x0F, 0x00, 0x00 })
                //{
                //    m.WriteMemory("BorderlandsGOTY.exe+1412B05", "bytes", "45 0F 57 E4 90 90 90 90 90");
                //}

                //if (ToggleStatus == OFF)
                //{
                //    m.WriteMemory("BorderlandsGOTY.exe+1412B05", "bytes", "45 0F 57 E4 90 90 90 90 90");
                //}

                //if (ToggleStatus == ON)
                //{
                //    m.WriteMemory("BorderlandsGOTY.exe+1412B05", "bytes", "F3 44 0F 59 A7 CC 0F 00 00");
                //}

                #endregion
            }
        }
        #endregion

        ///Buttons
        #region

        //Money Button
        private void MoneyButton_Click(object sender, EventArgs e)
        {
            if (MoneyTextBox.Text != "")
            {
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.Money}", "int", MoneyTextBox.Text);
            }
        }

        //XP Button
        private void XPButton_Click(object sender, EventArgs e)
        {
            if (XPTextBox.Text != "")
            {
                m.WriteMemory($"BorderlandsGOTY.exe+{Offsets.Level}", "int", XPTextBox.Text);
            }
        }

        //Golden Keys Button
        private void KeysButton_Click(object sender, EventArgs e)
        {
            /// -----> BUG REPORT : Golden Keys
            /// Sometimes sending keys doesnt work if player modifies Golden Keys Used offset. 
            /// If sending keys does not work , and sending a value higher than keys used also does not work. Reset Golden Keys Used to 0
            /// If more people have this issue I will adjust teh code to modify golden keys used as well as golden keys given . 
            /// I think this is related to sending a fixed number of keys rather than adding to current keys total. 
            /// More testing is required and I havebeen too busy messing with No Recoil and No Clip.
            
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
            //Defining Variables
            #region
            //Health
            var oldHealthValue = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.Health}");
            var newHealthValue = oldHealthValue;

            //Shields
            var oldShieldValue = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.Shield}");
            var newShieldValue = oldShieldValue;

            //Current Level
            var oldLevelValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.Level}");
            var newLevelValue = oldLevelValue;

            //Money
            var oldMoneyValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.Money}");
            var newMoneyValue = oldMoneyValue;

            //Experience
            var oldXPValue = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.Experience}");
            var newXPValue = oldXPValue;

            //SkillPoints
            var oldSkillPointsValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.SkillPoints}");
            var newSkillPointsValue = oldSkillPointsValue;

            //Golden Keys
            var oldKeysValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.GoldenKeyTotal}");
            var newKeysValue = oldKeysValue;

            //Golden Keys Used
            var oldUsedKeysValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.GoldenKeyUsed}");
            var newUsekdKeysValue = oldUsedKeysValue;

            //Skill Cooldown Timer
            var oldCooldownValue = m.ReadInt($"BorderlandsGOTY.exe+{Offsets.SkillCooldown}");
            var newCooldownValue = oldCooldownValue;

            //Ammo
            var maxValueGrenades = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.GrenadesMax}");
            var maxValueRepeater = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.RepeaterPistolMax}");
            var maxValueRevolver = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.RevolverAmmoMax}");
            var maxValueSMG = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.SMGAmmoMax}");
            var maxValueShells = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.ShotgunShellsMax}");
            var maxValueSniper = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.SniperRifleAmmoMax}");
            var maxValueLauncher = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.LauncherAmmoMax}");
            var maxValueCarbine = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.CarbineAmmoMax}");

            #endregion
            if (checkBox1.Checked)
            {
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Health}", "float", newHealthValue.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Shield}", "float", newShieldValue.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Level}", "int", newLevelValue.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Money}", "int", newMoneyValue.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Experience}", "float", newXPValue.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.SkillPoints}", "int", newSkillPointsValue.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.GoldenKeyTotal}", "int", newKeysValue.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.GoldenKeyUsed}", "int", newUsekdKeysValue.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.SkillCooldown}", "int", newCooldownValue.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.CarbineAmmo}", "float", maxValueCarbine.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Grenades}", "float", maxValueGrenades.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.RepeaterPistolAmmo}", "float", maxValueRepeater.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.RevolverAmmo}", "float", maxValueRevolver.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.SMGAmmo}", "float", maxValueSMG.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.ShotgunShells}", "float", maxValueShells.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.SniperRifleAmmo}", "float", maxValueSniper.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.LauncherAmmo}", "float", maxValueLauncher.ToString());
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
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.Grenades}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.RepeaterPistolAmmo}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.RevolverAmmo}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.SMGAmmo}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.ShotgunShells}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.SniperRifleAmmo}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.LauncherAmmo}");
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.CarbineAmmo}");
            }
        }

        //Unlimited Health , Shields and Ammo
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {   
            //Health
            var oldHealthValue = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.MaxHealth}");
            var newHealthValue = oldHealthValue;

            //Shields
            var oldShieldValue = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.MaxShield}");
            var newShieldValue = oldShieldValue;

            //Ammo
            var maxValueGrenades = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.GrenadesMax}");
            var maxValueRepeater = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.RepeaterPistolMax}");
            var maxValueRevolver = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.RevolverAmmoMax}");
            var maxValueSMG = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.SMGAmmoMax}");
            var maxValueShells = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.ShotgunShellsMax}");
            var maxValueSniper = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.SniperRifleAmmoMax}");
            var maxValueLauncher = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.LauncherAmmoMax}");
            var maxValueCarbine = m.ReadFloat($"BorderlandsGOTY.exe+{Offsets.CarbineAmmoMax}");

            if (checkBox2.Checked)
            {
                //Health & Shields
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Health}", "float", newHealthValue.ToString());
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.Shield}", "float", newShieldValue.ToString());

                //Ammo
                m.FreezeValue($"BorderlandsGOTY.exe+{Offsets.CarbineAmmo}", "float", maxValueCarbine.ToString());
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
                m.UnfreezeValue($"BorderlandsGOTY.exe+{Offsets.CarbineAmmo}");
            }
        }
        #endregion
    }
}
