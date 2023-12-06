using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using UmeboshiLibrary;

namespace JiroJudgeViewer {
    public partial class Form1 : Form {

        JiroDead jiroDead;
        private const string VERSION = "V0.5";

        private const string RESULT_FOLDER = @".\Result";
        private const string HOZONWAVE = @".\Wave\Hozon.wav";
        private const string P1DEFETEDWAVE = @".\Wave\p1die.wav";
        private const string P2DEFETEDWAVE = @".\Wave\p2die.wav";

        private System.Diagnostics.Process jiro;

        Shionamensa shionamensa;
        ShionamensaP2 shionamensaP2;

        private bool IsResult;

        private bool P1Defeted;
        private bool P2Defeted;

        private int OBAKAcount;

        // ���O�̔���
        private int OldGreatP1 = 0;
        private int OldGoodP1 = 0;
        private int OldBadP1 = 0;
        private int OldGreatP2 = 0;
        private int OldGoodP2 = 0;
        private int OldBadP2 = 0;

        // �n�[�h�Q�[�W���������������[�h�p
        int wHardGaugeValueP1 = 0;
        int wHardGaugeValueP2 = 0;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            jiroDead = new JiroDead();
            shionamensa = new Shionamensa();
            shionamensaP2 = new ShionamensaP2();
            OBAKAcount = 0;

            this.Text = "DP Judge Viewer " + VERSION;
            if (!Directory.Exists(RESULT_FOLDER)) {
                Directory.CreateDirectory(RESULT_FOLDER);
            }

            IsResult = false;
            P1Defeted = false;
            P2Defeted = false;

            this.TopMost = true;
            var jiros = System.Diagnostics.Process.GetProcessesByName("taikojiro");
            if (jiros.Length == 0) {
                MessageBox.Show("���Y�N�����Ƃ��");
            } else {

                jiro = jiros[0];
            }
            timer1.Interval = 16;
            timer1.Start();

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            cbHardGauge.SelectedIndex = 0;
        }

        private void ReadJiroMemory() {


            if (jiro == null) {
                var jiros = System.Diagnostics.Process.GetProcessesByName("taikojiro");
                if (jiros.Length == 0) {
                    this.Close();
                    return;
                }
                jiro = jiros[0];
            }
            if (jiro.HasExited) {
                if (!jiroDead.Visible) {
                    if (jiroDead.ShowDialog() == DialogResult.OK) {
                        this.Close();
                    }
                }

                return;
            }
            //if (jiro.ExitCode != 0) return;

            var jiroBaseAddress = (int)jiro.MainModule.BaseAddress;

            const int GREAT_P1_ADDRESS = 0x0007C8E8;
            const int READ_GREAT_P1_SIZE = 2;
            const int GOOD_P1_ADDRESS = 0x0007C8EA;
            const int READ_GOOD_P1_SIZE = 2;
            const int BAD_P1_ADDRESS = 0x0007C8EC;
            const int READ_BAD_P1_SIZE = 2;
            const int NOTES_P1_ADDRESS = 0x0007C7A4;
            const int READ_NOTES_P1_SIZE = 2;

            const int GREAT_P2_ADDRESS = 0x0007CAC8;
            const int READ_GREAT_P2_SIZE = 2;
            const int GOOD_P2_ADDRESS = 0x0007CACA;
            const int READ_GOOD_P2_SIZE = 2;
            const int BAD_P2_ADDRESS = 0x0007CACC;
            const int READ_BAD_P2_SIZE = 2;
            const int NOTES_P2_ADDRESS = 0x0007C984;
            const int READ_NOTES_P2_SIZE = 2;

            // �ʏ�m���}�Q�[�W�̗��܂�
            const int NORMAGAUGE_VALUE_P1_ADDRESS = 0x0007C778;
            const int READ_NORMAGAUGE_VALUE_P1_SIZE = 8;
            const int NORMAGAUGE_VALUE_P2_ADDRESS = 0x0007C958;
            const int READ_NORMAGAUGE_VALUE_P2_SIZE = 8;

            // �ʏ�m���}�Q�[�W�̏�ԁi���N���A�A�N���A�A�����j
            const int NORMAGAUGE_STATUS_P1_ADDRESS = 0x0007C7E8;
            const int READ_NORMAGAUGE_STATUS_P1_SIZE = 1;
            const int NORMAGAUGE_STATUS_P2_ADDRESS = 0x0007C9C8;
            const int READ_NORMAGAUGE_STATUS_P2_SIZE = 1;

            // �n�[�h�Q�[�W���ۂ�
            const int NORMAGAUGE_ADDRESS = 0x0007D322;
            const int READ_NORMAGAUGE_SIZE = 1;

            // �n�[�h�Q�[�W�̗��܂�
            // �i�ʃQ�[�W�̗��܂��H
            const int HARDGAUGE_VALUE_P1_ADDRESS = 0x0007C916;
            const int READ_HARDGAUGE_VALUE_P1_SIZE = 2;
            const int HARDGAUGE_VALUE_P2_ADDRESS = 0x0007CAF6;
            const int READ_HARDGAUGE_VALUE_P2_SIZE = 2;

            // �Q�[�W��0�ɂȂ��1�ɂȂ�
            const int HARDGAUGE_STATUS_P1_ADDRESS = 0x0007C750;
            const int READ_HARDGAUGE_STATUS_P1_SIZE = 1;
            const int HARDGAUGE_STATUS_P2_ADDRESS = 0x0007C930;
            const int READ_HARDGAUGE_STATUS_P2_SIZE = 1;

            // �i�ʂ��ۂ��i�i�ʂ̋Ȑ��i�i�ʂ���Ȃ��ꍇ�F0�j�j
            const int ISTJC_ADDRESS = 0x0007F080;
            const int READ_ISTJC_SIZE = 1;

            const int AUTO_P1_ADDRESS = 0x0007C908;
            const int READ_AUTO_P1_SIZE = 1;
            const int AUTO_P2_ADDRESS = 0x0007CAE8;
            const int READ_AUTO_P2_SIZE = 1;

            const int TJA_PATH_ADDRESS = 0x0007BB90;
            const int READ_TJA_PATH_SIZE = 256;

            const int HANTEI_SECONDS_ADDRESS = 0x00A8F0FC;
            const int HANTEI_SECONDS_SIZE = 2;

            const int ISPLAYING_ADDRESS = 0x0007E4E2;
            const int READ_ISPLAYING_SIZE = 1;

            byte[] GreatP1Array = new byte[READ_GREAT_P1_SIZE];
            byte[] GoodP1Array = new byte[READ_GOOD_P1_SIZE];
            byte[] BadP1Array = new byte[READ_BAD_P1_SIZE];
            byte[] NotesP1Array = new byte[READ_NOTES_P1_SIZE];

            byte[] GreatP2Array = new byte[READ_GREAT_P2_SIZE];
            byte[] GoodP2Array = new byte[READ_GOOD_P2_SIZE];
            byte[] BadP2Array = new byte[READ_BAD_P2_SIZE];
            byte[] NotesP2Array = new byte[READ_NOTES_P2_SIZE];

            byte[] NormaGaugeValueP1Array = new byte[READ_NORMAGAUGE_VALUE_P1_SIZE];
            byte[] NormaGaugeValueP2Array = new byte[READ_NORMAGAUGE_VALUE_P2_SIZE];

            byte[] NormaGaugeStatusP1Array = new byte[READ_NORMAGAUGE_STATUS_P1_SIZE];
            byte[] NormaGaugeStatusP2Array = new byte[READ_NORMAGAUGE_STATUS_P2_SIZE];

            byte[] NormaGaugeArray = new byte[READ_NORMAGAUGE_SIZE];

            byte[] HardGaugeStatusP1Array = new byte[READ_HARDGAUGE_STATUS_P1_SIZE];
            byte[] HardGaugeStatusP2Array = new byte[READ_HARDGAUGE_STATUS_P2_SIZE];

            byte[] HardGaugeValueP1Array = new byte[READ_HARDGAUGE_VALUE_P1_SIZE];
            byte[] HardGaugeValueP2Array = new byte[READ_HARDGAUGE_VALUE_P2_SIZE];

            byte[] IsTJCArray = new byte[READ_ISTJC_SIZE];

            byte[] AutoP1Array = new byte[READ_AUTO_P1_SIZE];
            byte[] AutoP2Array = new byte[READ_AUTO_P2_SIZE];

            byte[] TJAPathArray = new byte[READ_TJA_PATH_SIZE];

            byte[] HanteiArray = new byte[HANTEI_SECONDS_SIZE];

            byte[] IsPlayingArray = new byte[READ_ISPLAYING_SIZE];

            IntPtr GreatP1Ptr = new IntPtr(GREAT_P1_ADDRESS + jiroBaseAddress);
            IntPtr GoodP1Ptr = new IntPtr(GOOD_P1_ADDRESS + jiroBaseAddress);
            IntPtr BadP1Ptr = new IntPtr(BAD_P1_ADDRESS + jiroBaseAddress);
            IntPtr NotesP1Ptr = new IntPtr(NOTES_P1_ADDRESS + jiroBaseAddress);

            IntPtr GreatP2Ptr = new IntPtr(GREAT_P2_ADDRESS + jiroBaseAddress);
            IntPtr GoodP2Ptr = new IntPtr(GOOD_P2_ADDRESS + jiroBaseAddress);
            IntPtr BadP2Ptr = new IntPtr(BAD_P2_ADDRESS + jiroBaseAddress);
            IntPtr NotesP2Ptr = new IntPtr(NOTES_P2_ADDRESS + jiroBaseAddress);

            IntPtr NormaGaugeValueP1Ptr = new IntPtr(NORMAGAUGE_VALUE_P1_ADDRESS + jiroBaseAddress);
            IntPtr NormaGaugeValueP2Ptr = new IntPtr(NORMAGAUGE_VALUE_P2_ADDRESS + jiroBaseAddress);

            IntPtr NormaGaugeStatusP1Ptr = new IntPtr(NORMAGAUGE_STATUS_P1_ADDRESS + jiroBaseAddress);
            IntPtr NormaGaugeStatusP2Ptr = new IntPtr(NORMAGAUGE_STATUS_P2_ADDRESS + jiroBaseAddress);

            IntPtr NormaGaugePtr = new IntPtr(NORMAGAUGE_ADDRESS + jiroBaseAddress);

            IntPtr HardGaugeStatusP1Ptr = new IntPtr(HARDGAUGE_STATUS_P1_ADDRESS + jiroBaseAddress);
            IntPtr HardGaugeStatusP2Ptr = new IntPtr(HARDGAUGE_STATUS_P2_ADDRESS + jiroBaseAddress);

            IntPtr HardGaugeValueP1Ptr = new IntPtr(HARDGAUGE_VALUE_P1_ADDRESS + jiroBaseAddress);
            IntPtr HardGaugeValueP2Ptr = new IntPtr(HARDGAUGE_VALUE_P2_ADDRESS + jiroBaseAddress);

            IntPtr IsTJCPtr = new IntPtr(ISTJC_ADDRESS + jiroBaseAddress);

            IntPtr AutoP1Ptr = new IntPtr(AUTO_P1_ADDRESS + jiroBaseAddress);
            IntPtr AutoP2Ptr = new IntPtr(AUTO_P2_ADDRESS + jiroBaseAddress);

            IntPtr TjaPathPtr = new IntPtr(TJA_PATH_ADDRESS + jiroBaseAddress);

            IntPtr HanteiPtr = new IntPtr(HANTEI_SECONDS_ADDRESS + jiroBaseAddress);

            IntPtr IsPlayingPtr = new IntPtr(ISPLAYING_ADDRESS + jiroBaseAddress);

            int GreatP1Byte;
            int GoodP1Byte;
            int BadP1Byte;
            int NotesP1Byte;

            int GreatP2Byte;
            int GoodP2Byte;
            int BadP2Byte;
            int NotesP2Byte;

            int NormaGaugeValueP1Byte;
            int NormaGaugeValueP2Byte;

            int NormaGaugeStatusP1Byte;
            int NormaGaugeStatusP2Byte;

            int NormaGaugeByte;

            int HardGaugeStatusP1Byte;
            int HardGaugeStatusP2Byte;

            int HardGaugeValueP1Byte;
            int HardGaugeValueP2Byte;

            int IsTJCByte;

            int AutoP1Byte;
            int AutoP2Byte;

            int TJAPathByte;

            int IsPlayingByte;

            if (!Win32Api.ReadProcessMemory(jiro.Handle, GreatP1Ptr, GreatP1Array, READ_GREAT_P1_SIZE, out GreatP1Byte)) {
                LbGrP1.Text = $"�擾�ł��܂���ł���";
                return;
            }
            if (!Win32Api.ReadProcessMemory(jiro.Handle, GoodP1Ptr, GoodP1Array, READ_GOOD_P1_SIZE, out GoodP1Byte)) {
                LbGdP1.Text = $"�擾�ł��܂���ł���";
                return;
            }
            if (!Win32Api.ReadProcessMemory(jiro.Handle, BadP1Ptr, BadP1Array, READ_BAD_P1_SIZE, out BadP1Byte)) {
                LbBdP1.Text = $"�擾�ł��܂���ł���";
                return;
            }
            if (!Win32Api.ReadProcessMemory(jiro.Handle, NotesP1Ptr, NotesP1Array, READ_NOTES_P1_SIZE, out NotesP1Byte)) {
                LbBdP1.Text = $"�擾�ł��܂���ł���";
                return;
            }


            if (!Win32Api.ReadProcessMemory(jiro.Handle, GreatP2Ptr, GreatP2Array, READ_GREAT_P2_SIZE, out GreatP2Byte)) {
                LbGrP2.Text = $"�擾�ł��܂���ł���";
                return;
            }
            if (!Win32Api.ReadProcessMemory(jiro.Handle, GoodP2Ptr, GoodP2Array, READ_GOOD_P2_SIZE, out GoodP2Byte)) {
                LbGdP2.Text = $"�擾�ł��܂���ł���";
                return;
            }
            if (!Win32Api.ReadProcessMemory(jiro.Handle, BadP2Ptr, BadP2Array, READ_BAD_P2_SIZE, out BadP2Byte)) {
                LbBdP2.Text = $"�擾�ł��܂���ł���";
                return;
            }
            if (!Win32Api.ReadProcessMemory(jiro.Handle, NotesP2Ptr, NotesP2Array, READ_NOTES_P2_SIZE, out NotesP2Byte)) {
                LbBdP2.Text = $"�擾�ł��܂���ł���";
                return;
            }

            Win32Api.ReadProcessMemory(jiro.Handle, TjaPathPtr, TJAPathArray, READ_TJA_PATH_SIZE, out TJAPathByte);

            // ��ʐ��ڂ�����
            // 0 : �v���C�O
            // 1 : �v���C��
            // 2 : ���U���g�m��
            // 3 : ���U���g���
            Win32Api.ReadProcessMemory(jiro.Handle, IsPlayingPtr, IsPlayingArray, READ_ISPLAYING_SIZE, out IsPlayingByte);

            // �m���}�Q�[�W�擾
            Win32Api.ReadProcessMemory(jiro.Handle, NormaGaugeValueP1Ptr, NormaGaugeValueP1Array, READ_NORMAGAUGE_VALUE_P1_SIZE, out NormaGaugeValueP1Byte);
            Win32Api.ReadProcessMemory(jiro.Handle, NormaGaugeValueP2Ptr, NormaGaugeValueP2Array, READ_NORMAGAUGE_VALUE_P2_SIZE, out NormaGaugeValueP2Byte);
            Win32Api.ReadProcessMemory(jiro.Handle, NormaGaugeStatusP1Ptr, NormaGaugeStatusP1Array, READ_NORMAGAUGE_STATUS_P1_SIZE, out NormaGaugeStatusP1Byte);
            Win32Api.ReadProcessMemory(jiro.Handle, NormaGaugeStatusP2Ptr, NormaGaugeStatusP2Array, READ_NORMAGAUGE_STATUS_P2_SIZE, out NormaGaugeStatusP2Byte);

            // �m���}�Q�[�W�i�n�[�h�Q�[�W���ۂ��j�擾
            Win32Api.ReadProcessMemory(jiro.Handle, NormaGaugePtr, NormaGaugeArray, READ_NORMAGAUGE_SIZE, out NormaGaugeByte);

            // �n�[�h�Q�[�W�擾
            Win32Api.ReadProcessMemory(jiro.Handle, HardGaugeStatusP1Ptr, HardGaugeStatusP1Array, READ_HARDGAUGE_STATUS_P1_SIZE, out HardGaugeStatusP1Byte);
            Win32Api.ReadProcessMemory(jiro.Handle, HardGaugeStatusP2Ptr, HardGaugeStatusP2Array, READ_HARDGAUGE_STATUS_P2_SIZE, out HardGaugeStatusP2Byte);
            Win32Api.ReadProcessMemory(jiro.Handle, HardGaugeValueP1Ptr, HardGaugeValueP1Array, READ_HARDGAUGE_VALUE_P1_SIZE, out HardGaugeValueP1Byte);
            Win32Api.ReadProcessMemory(jiro.Handle, HardGaugeValueP2Ptr, HardGaugeValueP2Array, READ_HARDGAUGE_VALUE_P2_SIZE, out HardGaugeValueP2Byte);

            // �i�ʃQ�[�W��
            Win32Api.ReadProcessMemory(jiro.Handle, IsTJCPtr, IsTJCArray, READ_ISTJC_SIZE, out IsTJCByte);

            //�I�[�g���
            Win32Api.ReadProcessMemory(jiro.Handle, AutoP1Ptr, AutoP1Array, READ_AUTO_P1_SIZE, out AutoP1Byte);
            Win32Api.ReadProcessMemory(jiro.Handle, AutoP2Ptr, AutoP2Array, READ_AUTO_P2_SIZE, out AutoP2Byte);

            bool isAutoP1 = AutoP1Array[0] == 1;
            bool isAutoP2 = AutoP2Array[0] == 1;
            //if (!Win32Api.ReadProcessMemory(jiro.Handle, HanteiPtr, HanteiArray, READ_NOTES_SIZE, out NotesByte)) {
            //    LbBd.Text = $"�擾�ł��܂���ł���";
            //    return;
            //}

            //int great = Win32Api.ReadProcessMemory(jiro.Handle, GreatPtr, GreatResultArrayPtr, READ_GREAT_SIZE, out GreatByte);

            // �e����̒l���擾
            int GreatP1 = GreatP1Array[1] * 256 + GreatP1Array[0];
            int GoodP1 = GoodP1Array[1] * 256 + GoodP1Array[0];
            int BadP1 = BadP1Array[1] * 256 + BadP1Array[0];
            int NotesP1 = NotesP1Array[1] * 256 + NotesP1Array[0];
            int SbNotesP1 = NotesP1 - (GreatP1 + GoodP1 + BadP1);

            double ScoreP1 = RoundDown((double)(GreatP1 + GoodP1 * 0.5) / NotesP1 * 100, 2);

            int GreatP2 = GreatP2Array[1] * 256 + GreatP2Array[0];
            int GoodP2 = GoodP2Array[1] * 256 + GoodP2Array[0];
            int BadP2 = BadP2Array[1] * 256 + BadP2Array[0];
            int NotesP2 = NotesP2Array[1] * 256 + NotesP2Array[0];
            int SbNotesP2 = NotesP2 - (GreatP2 + GoodP2 + BadP2);

            double ScoreP2 = RoundDown((double)(GreatP2 + GoodP2 * 0.5) / NotesP2 * 100, 2);

            double TotalScore = RoundDown((double)(GreatP1 + GoodP1 * 0.5 + GreatP2 + GoodP2 * 0.5) / (NotesP1 + NotesP2) * 100, 2);

            int EXScore = GreatP1 * 2 + GoodP1 + GreatP2 * 2 + GoodP2;
            int MAXMinus = GoodP1 + BadP1 * 2 + GoodP2 + BadP2 * 2;

            // �e����̒l���ω��������ǂ���
            bool GreatP1Changed = OldGreatP1 != GreatP1;
            bool GoodP1Changed = OldGoodP1 != GoodP1;
            bool BadP1Changed = OldBadP1 != BadP1;
            bool GreatP2Changed = OldGreatP2 != GreatP2;
            bool GoodP2Changed = OldGoodP2 != GoodP2;
            bool BadP2Changed = OldBadP2 != BadP2;

            // �n�[�h�Q�[�W�̋�����������
            // �n�[�h�Q�[�W�̏����l�ƗǁA�A�s�̑����l
            int HardGaugeInit = 10000;
            int GreatIncDec = 0;
            int GoodIncDec = 0;
            int BadIncDec = 0;
            switch (cbHardGauge.SelectedIndex) {
                // �ʏ�n�[�h�Q�[�W
                case 0:
                    break;
                // ���n�[�h�Q�[�W
                case 1:
                    GreatIncDec = 16;
                    GoodIncDec = -100;
                    BadIncDec = -1200;
                    break;
                // �o�J�n�[�h�Q�[�W
                case 2:
                    GreatIncDec = 16;
                    GoodIncDec = -250;
                    BadIncDec = -3600;
                    break;
                // �񕜂Ȃ��n�[�h�Q�[�W
                case 3:
                    GreatIncDec = 0;
                    GreatIncDec = 0;
                    BadIncDec = -900;
                    break;
                // �t���A�Q�[�W
                case 4:
                    GreatIncDec = 0;
                    GoodIncDec = -300;
                    BadIncDec = -3334;
                    break;
                // �񕜂Ȃ��t���AEX�Q�[�W
                case 5:
                    GreatIncDec = 0;
                    GoodIncDec = -1000;
                    BadIncDec = -5000;
                    break;
                // �̂݃Q�[�W
                case 6:
                    GreatIncDec = -400;
                    GoodIncDec = 50;
                    BadIncDec = -400;
                    break;
                default:
                    break;
            }


            // �n�[�h�Q�[�W���������������[�h
            if (cbHardGauge.SelectedIndex != 0) {

                // �v���C������Ȃ���΃Q�[�W�͏����l��
                if (IsPlayingArray[0] == 0) {
                    wHardGaugeValueP1 = HardGaugeInit;
                    wHardGaugeValueP2 = HardGaugeInit;
                }

                if (wHardGaugeValueP1 < 0) wHardGaugeValueP1 = 0;
                if (wHardGaugeValueP1 > 10000) wHardGaugeValueP1 = 10000;
                if (wHardGaugeValueP2 > 10000) wHardGaugeValueP2 = 10000;
                if (wHardGaugeValueP2 < 0) wHardGaugeValueP2 = 0;

                // �ω���������ɍ��킹�Ċe�Q�[�W��ω�������
                if (GreatP1Changed) {
                    wHardGaugeValueP1 += GreatIncDec;
                }
                if (GoodP1Changed) {
                    wHardGaugeValueP1 += GoodIncDec;
                }
                if (BadP1Changed) {
                    wHardGaugeValueP1 += BadIncDec;
                }
                if (GreatP2Changed) {
                    wHardGaugeValueP2 += GreatIncDec;
                }
                if (GoodP2Changed) {
                    wHardGaugeValueP2 += GoodIncDec;
                }
                if (BadP2Changed) {
                    wHardGaugeValueP2 += BadIncDec;
                }

                if(CbIfDowned.Checked == true) {
                    if(wHardGaugeValueP1 == 0) wHardGaugeValueP2 = 0;
                    if(wHardGaugeValueP2 == 0) wHardGaugeValueP1 = 0;
                }

                // �������݂����n�[�h�Q�[�W�̗�
                var wHardGaugeValueBytesP1 = BitConverter.GetBytes(wHardGaugeValueP1);

                // �������݂����n�[�h�Q�[�W�̗�
                var wHardGaugeValueBytesP2 = BitConverter.GetBytes(wHardGaugeValueP2);

                // ���Y�̃������Ƀn�[�h�Q�[�W�̃������l�ɒl���������ނ�
                Win32Api.WriteProcessMemory(jiro.Handle, HardGaugeValueP1Ptr, wHardGaugeValueBytesP1, READ_HARDGAUGE_VALUE_P1_SIZE, out HardGaugeValueP1Byte);
                Win32Api.WriteProcessMemory(jiro.Handle, HardGaugeValueP2Ptr, wHardGaugeValueBytesP2, READ_HARDGAUGE_VALUE_P2_SIZE, out HardGaugeValueP2Byte);

            }

            // ���t����tja�t���p�X
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); // memo: Shift-JIS���������߂̂��܂��Ȃ�
            string TJAPath = Encoding.GetEncoding("shift_jis").GetString(TJAPathArray);
            if (!TJAPath.Contains(".tja") && !TJAPath.Contains(".tjc")) {
                return;
            }
            if (TJAPath.Contains(".tja")) {
                TJAPath = TJAPath.Substring(0, TJAPath.IndexOf(@".tja") + 4);
            } else if (TJAPath.Contains(".tjc")) {
                TJAPath = TJAPath.Substring(0, TJAPath.IndexOf(@".tjc") + 4);
            }

            TJA now_tja = new TJA(new FileInfo(TJAPath));

            double NormaGaugeValueP1 = BitConverter.ToDouble(NormaGaugeValueP1Array, 0);
            double NormaGaugeValueP2 = BitConverter.ToDouble(NormaGaugeValueP2Array, 0);

            double GOGOScoreP1 = (double)now_tja.NotesListP1.Where(x => x.IsGOGO).Count() * 1.2 + now_tja.NotesListP1.Where(x => x.IsGOGO == false).Count();
            double GOGOScoreP2 = (double)now_tja.NotesListP2.Where(x => x.IsGOGO).Count() * 1.2 + now_tja.NotesListP2.Where(x => x.IsGOGO == false).Count();

            // �m���}�Q�[�W�̗��_�l���v�Z����
            double NormaGaugeValueMAXP1;
            double NormaGaugeValueMAXP2;
            if (now_tja.LEVEL >= 9) {
                if (now_tja.TOTAL == 0) {
                    NormaGaugeValueMAXP1 = (GOGOScoreP1) * 1.4 + RoundDown(GOGOScoreP1 * 0.1, 1);
                    NormaGaugeValueMAXP2 = (GOGOScoreP2) * 1.4 + RoundDown(GOGOScoreP2 * 0.1, 1);
                } else {
                    NormaGaugeValueMAXP1 = (GOGOScoreP1) * (200 / now_tja.TOTAL);
                    NormaGaugeValueMAXP2 = (GOGOScoreP2) * (200 / now_tja.TOTAL);
                }
            } else {
                if (now_tja.TOTAL == 0) {
                    NormaGaugeValueMAXP1 = (GOGOScoreP1) * 1.4;
                    NormaGaugeValueMAXP2 = (GOGOScoreP2) * 1.4;
                } else {
                    NormaGaugeValueMAXP1 = (GOGOScoreP1) * (200 / now_tja.TOTAL);
                    NormaGaugeValueMAXP2 = (GOGOScoreP2) * (200 / now_tja.TOTAL);
                }

            }


            double NormaGaugeProbP1 = NormaGaugeValueP1;
            double HardGaugeValueP1 = ((double)HardGaugeValueP1Array[1] * 256 + (double)HardGaugeValueP1Array[0]) / 100;
            double HardGaugeValueP2 = ((double)HardGaugeValueP2Array[1] * 256 + (double)HardGaugeValueP2Array[0]) / 100;


            double UpScore = RoundDown((double)(GreatP1 + GoodP1 * 0.5) / (double)NotesP1 * 1000000, 0);
            double Rating = 0;


            if (TotalScore < 50) Rating = 0;
            else if (TotalScore < 70) Rating = now_tja.LEVEL * (TotalScore - 50) / 20;
            else Rating = now_tja.LEVEL + (TotalScore - 70) / 10;
            if (SbNotesP1 == 0) {
                Rating += GetBonus(TotalScore, now_tja.LEVEL, BadP1);
            }
            Rating = RoundDown(Rating, 2);

            double DownScoreP1 = GoodP1 == 0 && BadP1 == 0 ? 1000000 : RoundDown((double)(GreatP1 + GoodP1 * 0.5) / (double)NotesP1 * 1000000 + (SbNotesP1) / (double)NotesP1 * 1000000, 0);
            double DownScoreP2 = GoodP2 == 0 && BadP2 == 0 ? 1000000 : RoundDown((double)(GreatP2 + GoodP2 * 0.5) / (double)NotesP2 * 1000000 + (SbNotesP2) / (double)NotesP2 * 1000000, 0);

            if (IsPlayingArray[0] != 3) {
                IsResult = false;
            } else if (IsPlayingArray[0] == 3) {
                // �i�ʒ��łȂ��ꍇ
                if (IsResult == false && NotesP1 != 1 && NotesP2 != 1 && ScoreP1 != 0 && ScoreP2 != 0 && IsTJCArray[0] == 0) {
                    IsResult = true;
                    string TJAFolder = now_tja.TJAPath.Directory.Name;
                    string ResultFileName = now_tja.TJAPath.Name;
                    ResultFileName = Path.ChangeExtension(ResultFileName, ".xml");

                    ResultType resultType = ResultType.NOTCLEAR;
                    if (NormaGaugeStatusP1Array[0] == 1 && NormaGaugeStatusP2Array[0] == 1) {
                        resultType = ResultType.CLEAR;
                    }
                    if (NormaGaugeStatusP1Array[0] == 2 && NormaGaugeStatusP2Array[0] == 2) {
                        resultType = ResultType.GAUGEMAX;
                    }
                    if (NormaGaugeArray[0] == 1 && HardGaugeStatusP1Array[0] == 0 && HardGaugeStatusP2Array[0] == 0) {
                        resultType = ResultType.HARDCLEAR;
                    }
                    if (BadP1 == 0 && BadP2 == 0) {
                        resultType = ResultType.FULLCOMBO;
                    }
                    if (ScoreP1 == 100 && ScoreP2 == 100) {
                        resultType = ResultType.ALLPERFECT;
                    }

                    Result result = new Result(now_tja.TITLE, now_tja.LEVEL, GreatP1, GoodP1, BadP1, ScoreP1, GreatP2, GoodP2, BadP2, ScoreP2, TotalScore, EXScore, MAXMinus, resultType);
                    if (!isAutoP1 && !isAutoP2) {
                        if (File.Exists(HOZONWAVE) && Result.Write(result, Path.Combine(RESULT_FOLDER, TJAFolder), ResultFileName) == true) {
                            SoundPlayer soundPlayer = new SoundPlayer(HOZONWAVE);
                            soundPlayer.Play();
                            soundPlayer.Dispose();
                        }
                    }
                    if (HardGaugeValueP1 - HardGaugeValueP2 >= 20) {
                        if (shionamensa.Disposing || !shionamensa.Visible) {
                            shionamensa = new Shionamensa();
                            shionamensa.Show();
                        }
                    } else if (HardGaugeValueP2 - HardGaugeValueP1 >= 20) {
                        if (shionamensaP2.Disposing || !shionamensaP2.Visible) {
                            shionamensaP2 = new ShionamensaP2();
                            shionamensaP2.Show();
                        }
                    }
                    // �V�I�A�l�L�������ꍇ
                } else if (IsResult == false && (HardGaugeValueP1 - HardGaugeValueP2 >= 20)) {
                    IsResult = true;
                    if (shionamensa.Disposing || !shionamensa.Visible) {
                        shionamensa = new Shionamensa();
                        shionamensa.Show();
                    }
                } else if (IsResult == false && HardGaugeValueP2 - HardGaugeValueP1 >= 20) {
                    IsResult = true;
                    if (shionamensaP2.Disposing || !shionamensaP2.Visible) {
                        shionamensaP2 = new ShionamensaP2();
                        shionamensaP2.Show();
                    }
                } else if (IsResult == true && ScoreP1 == 0 && ScoreP2 == 0) {
                    IsResult = false;
                }
            }

            LbISRESULT.Text = IsResult ? $"TRUE" : $"FALSE";

            LbGrP1.Text = $"{GreatP1}";
            LbGdP1.Text = $"{GoodP1}";
            LbBdP1.Text = $"{BadP1}";
            LbSbNotesP1.Text = $"{NotesP1 - (GreatP1 + GoodP1 + BadP1)}";

            LbScoreP1.Text = $"{ScoreP1:N2}%";

            LbGrP2.Text = $"{GreatP2}";
            LbGdP2.Text = $"{GoodP2}";
            LbBdP2.Text = $"{BadP2}";
            LbSbNotesP2.Text = $"{NotesP2 - (GreatP2 + GoodP2 + BadP2)}";

            LbScoreP2.Text = $"{ScoreP2:N2}%";

            LbTotalScore.Text = $"{TotalScore:N2}%";

            LbRating.Text = $"{Rating:0.00}";

            LbExScore.Text = EXScore.ToString();
            LbMAXMinus.Text = MAXMinus.ToString();

            // �i�ʃv���C���i�Ȑ����j
            if (IsTJCArray[0] != 0) {
                LbGaugeStatusP1.Text = HardGaugeStatusP1Array[0] == 0 ? $"�p�����i{HardGaugeValueP1:N2}%�j" : "�����܂���������";
                LbGaugeStatusP2.Text = HardGaugeStatusP2Array[0] == 0 ? $"�p�����i{HardGaugeValueP2:N2}%�j" : "�����܂���������";
                // �n�[�h�Q�[�W
            } else if (NormaGaugeArray[0] == 1) {
                LbGaugeStatusP1.Text = HardGaugeStatusP1Array[0] == 0 ? $"�p�����i{HardGaugeValueP1:N2}%�j" : "�����܂���������";
                LbGaugeStatusP2.Text = HardGaugeStatusP2Array[0] == 0 ? $"�p�����i{HardGaugeValueP2:N2}%�j" : "�����܂���������";

                // �m�[�}���Q�[�W
            } else {
                double NormaGaugeShowP1 = NormaGaugeValueP1 / NormaGaugeValueMAXP1 * 100 >= 99.9 ? 100 : NormaGaugeValueP1 / NormaGaugeValueMAXP1 * 100;
                double NormaGaugeShowP2 = NormaGaugeValueP2 / NormaGaugeValueMAXP2 * 100 >= 99.9 ? 100 : NormaGaugeValueP2 / NormaGaugeValueMAXP2 * 100;


                //LbGaugeStatusP1.Text = $"�Q�[�W{NormaGaugeValueP1}%";
                //LbGaugeStatusP2.Text = $"�Q�[�W{NormaGaugeValueP2}%";
                switch (NormaGaugeStatusP1Array[0]) {
                    case 0:
                    case 1:
                        LbGaugeStatusP1.Text = $"�Q�[�W{NormaGaugeShowP1:N2}%";
                        break;
                    case 2:
                        LbGaugeStatusP1.Text = "����";
                        break;
                    default:
                        LbGaugeStatusP1.Text = "���H";
                        break;
                }
                switch (NormaGaugeStatusP2Array[0]) {
                    case 0:
                    case 1:
                        LbGaugeStatusP2.Text = $"�Q�[�W{NormaGaugeShowP2:N2}%";
                        break;
                    case 2:
                        LbGaugeStatusP2.Text = "����";
                        break;
                    default:
                        LbGaugeStatusP2.Text = "���H";
                        break;
                }
            }

            OldGreatP1 = GreatP1;
            OldGoodP1 = GoodP1;
            OldBadP1 = BadP1;
            OldGreatP2 = GreatP2;
            OldGoodP2 = GoodP2;
            OldBadP2 = BadP2;

            if (HardGaugeStatusP1Array[0] != 0 && P1Defeted == false) {
                P1Defeted = true;
                SoundPlayer soundPlayer = new SoundPlayer(P1DEFETEDWAVE);
                soundPlayer.Play();
                soundPlayer.Dispose();
            }
            if (HardGaugeStatusP2Array[0] != 0 && P2Defeted == false) {
                P2Defeted = true;
                SoundPlayer soundPlayer = new SoundPlayer(P2DEFETEDWAVE);
                soundPlayer.Play();
                soundPlayer.Dispose();
            }

            if (HardGaugeStatusP1Array[0] == 0 && P1Defeted) P1Defeted = false;
            if (HardGaugeStatusP2Array[0] == 0 && P2Defeted) P2Defeted = false;

        }

        private double GetBonus(double score, double level, int bad) {
            double bonus = 0;

            // �t���R���{�{�[�i�X�A�S�ǃ{�[�i�X�̔{��
            const double BonusBaseRate = 9.0;
            const double FCBonusOddsUnder = 0.6;
            const double FCBonusOddsUpper = 0.175;
            const double APBonusOddsUnder = 1.6;
            const double APBonusOddsUpper = 1.0;

            if (score == 0) return 0;
            if (bad == 0) {
                if (level < 3) bonus = 0.25;
                else if (level == BonusBaseRate) bonus = 0.25;
                else if (level < BonusBaseRate) bonus = (BonusBaseRate - level) * FCBonusOddsUnder + 0.25;
                else if (level > BonusBaseRate) bonus = (level - BonusBaseRate) * FCBonusOddsUpper + 0.25;
                //} else if (score == 1000000) {
                //    if (level < 3) bonus = 0;
                //    else if (level == BonusBaseRate) bonus = 0;
                //    else if (level < BonusBaseRate) bonus = (BonusBaseRate - level) * APBonusOddsUnder;
                //    else if (level > BonusBaseRate) bonus = (level - BonusBaseRate) * APBonusOddsUpper;
                //}
            }
            if (score == 100) {
                if (level < 3) bonus += 0;
                bonus += 0.4;
            }

            return bonus;
        }

        private string GetRank(double Score) {

            if (Score >= 1000000) return "SS";
            else if (Score >= 995000) return "S+";
            else if (Score >= 990000) return "S";
            else if (Score >= 980000) return "AAA+";
            else if (Score >= 970000) return "AAA";
            else if (Score >= 960000) return "AA+";
            else if (Score >= 950000) return "AA";
            else if (Score >= 925000) return "A+";
            else if (Score >= 900000) return "A";
            else if (Score >= 850000) return "B+";
            else if (Score >= 800000) return "B";
            else if (Score >= 750000) return "C+";
            else if (Score >= 700000) return "C";
            else if (Score >= 650000) return "D+";
            else if (Score >= 600000) return "D";
            else if (Score >= 500000) return "E";
            else return "F";

        }

        /// <summary>
        /// �����_n�ʂŐ؂�̂Ăɂ��܂�
        /// </summary>
        /// <param name="val"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private double RoundDown(double val, int n) {
            var num = val * Math.Pow(10, n);
            num = Math.Floor(num);
            num /= Math.Pow(10, n);
            return num;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            ReadJiroMemory();
        }

        private void Form1_Shown(object sender, EventArgs e) {
            this.TopMost = true;
        }

        private void BtResultOutput_Click(object sender, EventArgs e) {
            OBAKAcount++;
            switch (OBAKAcount) {
                case 1:
                    MessageBox.Show("���肾���āI�I�I�I�I�I�I�I�I�o�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�J");
                    break;
                case 2:
                    MessageBox.Show("���肾�����Ă񂾂�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
                    break;
                case 3:
                    MessageBox.Show("�����I�I�I�I�I�I�I�I�I�I�I�b�𕷂����āI�I�I�I�I�I�I�I�I");
                    break;
                case 4:
                    MessageBox.Show("�������Ă�u�Ȃ�Ȃ񂾁v");
                    break;
                case 5:
                    MessageBox.Show("�f�f�[�� ������ �^�C�L�b�N");
                    break;
                case 6:
                    MessageBox.Show("�O����");
                    break;
                case 7:
                    MessageBox.Show("��΂��I�I�I�I�I�I�I�I�I�I�I�I�I�I�I���̑�Q�����[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[���[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[�{�[");
                    break;
                case 8:
                    MessageBox.Show("��΂ɔj�󂷂�A�L���X�F VS �Ȃ�ł��؂�Ȃ��S��");
                    break;
                case 9:
                    MessageBox.Show("��������̓X�^�b�t�N���W�b�g�������肵�܂�");
                    break;
                case 10:
                    MessageBox.Show("����\r\n��");
                    break;
                case 11:
                    MessageBox.Show("�X�y�V�����T���N�X\r\n���i�撣�����̂Łj");
                    break;
                case 12:
                    MessageBox.Show("Z00����\r\nZ00����");
                    break;
                case 13:
                    MessageBox.Show("�����w�����I�ψ���łƂ����ܖ�N���N�V����\r\n�v�[");
                    break;
                case 14:
                    MessageBox.Show("OP�EED��搧��\r\n���˂���");
                    break;
                case 15:
                    MessageBox.Show("�Ȃ񂩕s���F�������Ă�");
                    break;
                case 16:
                    MessageBox.Show("���{����\r\n�����܂���");
                    break;
                case 17:
                    MessageBox.Show("�����̂ق���\r\n�����m��");
                    break;
                case 18:
                    MessageBox.Show("�G�r�`���[�n��\r\n����");
                    break;
                case 19:
                    MessageBox.Show("DEF\r\n�W�T");
                    break;
                case 20:
                    MessageBox.Show("�p�`���R\r\n�p�`���R��");
                    break;
                case 21:
                    MessageBox.Show("���O�c�@\r\n�Q�c�@��");
                    break;
                case 22:
                    MessageBox.Show("�Ă�����\r\n���ːF");
                    break;
                case 23:
                    MessageBox.Show("���b�L�[�J���[\r\n�ԈȊO");
                    break;
                case 24:
                    MessageBox.Show("���ːF\r\n�Ă�����");
                    break;
                case 25:
                    MessageBox.Show("���Ȉ�\r\n��c�W��C���v�����g");
                    break;
                case 26:
                    MessageBox.Show("���q\r\n�p��");
                    break;
                case 27:
                    MessageBox.Show("AND YOU");
                    break;
                case 28:
                    MessageBox.Show("�X�^�b�t�N���W�b�g7��\r\nZ00");
                    break;
                case 30:
                    MessageBox.Show("1�񉽂��N���Ȃ��^�C�~���O���@��");
                    break;
                case 31:
                    MessageBox.Show("�X�^�b�t�N���W�b�g�͈ȏ�ł�");
                    break;
                case 32:
                    MessageBox.Show("����N���b�N���Ƃ�˂�@���@�o�@�J");
                    break;
                case 33:
                    MessageBox.Show("�g�I�����P�Q");
                    break;
                case 34:
                    MessageBox.Show("��΂��I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�Q�L���o�J�E���g�_�E��");
                    break;
                case 35:
                    MessageBox.Show("�J");
                    break;
                case 36:
                    MessageBox.Show("�n");
                    break;
                case 37:
                    MessageBox.Show("�Q�O");
                    break;
                case 38:
                    MessageBox.Show("�P�X");
                    break;
                case 39:
                    MessageBox.Show("�P�W");
                    break;
                case 40:
                    MessageBox.Show("�P�V");
                    Shiomaneki shiomaneki = new Shiomaneki();
                    shiomaneki.Show();
                    break;
                case 41:
                    MessageBox.Show("�P�U");
                    break;
                case 42:
                    MessageBox.Show("�P�T");
                    break;
                case 43:
                    MessageBox.Show("�P�S");
                    break;
                case 44:
                    MessageBox.Show("�P�R");
                    break;
                case 45:
                    MessageBox.Show("�P�Q");
                    break;
                case 46:
                    MessageBox.Show("�P�P");
                    break;
                case 47:
                    MessageBox.Show("�P�O");
                    break;
                case 48:
                    MessageBox.Show("�X");
                    Shionamensa shionamensa = new Shionamensa();
                    shionamensa.Show();
                    break;
                case 49:
                    MessageBox.Show("�W");
                    break;
                case 50:
                    MessageBox.Show("�V");
                    break;
                case 51:
                    MessageBox.Show("�U");
                    break;
                case 52:
                    MessageBox.Show("�T");
                    break;
                case 53:
                    MessageBox.Show("��΂��I�I�I�I�I���ƂT��؂������āI�I�I�I�I�I�I�I");
                    break;
                case 54:
                    MessageBox.Show("�����Ő؂�グ�Ȃ��Ƃ܂�����I�I�I�I�I");
                    break;
                case 55:
                    MessageBox.Show("�S");
                    break;
                case 56:
                    MessageBox.Show("�R");
                    break;
                case 57:
                    MessageBox.Show("�Q");
                    break;
                case 58:
                    MessageBox.Show("�P");
                    break;
                case 59:
                    MessageBox.Show("Are you ready�H");
                    break;
                case 60:
                    MessageBox.Show("�R");
                    break;
                case 61:
                    MessageBox.Show("�Q");
                    break;
                case 62:
                    MessageBox.Show("�P");
                    break;
                case 63:
                    MessageBox.Show("�C�N");
                    break;
                case 64:
                    MessageBox.Show("�Ȃ�ł��̃{�^��60����N���b�N���Ă��");
                    break;
                case 65:
                    MessageBox.Show("������Ԋ댯");
                    break;
                case 66:
                    MessageBox.Show("�o�C�o�C");
                    this.Close();
                    break;
                default:
                    MessageBox.Show("�����Ȃɂ��Ȃ���");
                    break;
            }
        }

        private void LbTotalScore_Click(object sender, EventArgs e) {

        }

        // ���t���ʂ̃R�s�[
        private void BtResultCopy_Click(object sender, EventArgs e) {
            Process[] jiro = Process.GetProcessesByName("taikojiro");
            if (jiro.Length == 0) return;
            string jirotitle = jiro[0].MainWindowTitle;
            if (!jirotitle.Contains(">>")) {
                MessageBox.Show("�I�ȉ�ʂł�");
                return;
            }
            if (LbExScore.Text == "0" && LbMAXMinus.Text == "0") {
                MessageBox.Show("���t�O�ł�");
                return;
            }
            string a = UmeboshiString.CutToStart(jirotitle, "Level :");
            string Title = CutToEnd(a, ">>").Trim();
            // �R�s�[���镶����̒�`
            string CopyString = $"TITLE:{Title}\r\n" +
                                $"ExScore:{LbExScore.Text} MAX-{LbMAXMinus.Text}";
            Clipboard.SetText(CopyString);
            // TODO:�O���b�v�{�[�h�ɃR�s�[
            MessageBox.Show("���݂̃��U���g���O���b�v�{�[�h�ɃR�s�[���܂����B");
        }

        public string CutToEnd(string line, string selectstring) {
            if (!line.Contains(selectstring)) {
                return line;
            }
            return line.Substring(line.IndexOf(selectstring) + selectstring.Length);
        }
    }
}