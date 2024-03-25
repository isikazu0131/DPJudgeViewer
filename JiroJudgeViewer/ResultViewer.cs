using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace JiroJudgeViewer {
    public partial class ResultViewer : Form {

        /// <summary>
        /// リザルトファイルが格納されたフォルダ
        /// </summary>
        private string RESULT_FOLDER = @".\Result";

        /// <summary>
        /// リザルト群
        /// </summary>
        private List<Result> results = new List<Result>();

        /// <summary>
        /// この画面の設定
        /// </summary>
        private ResultViewerSetting setting;

        public ResultViewer() {
            InitializeComponent();
        }

        private void ResultViewer_Load(object sender, EventArgs e) {
            if (!Directory.Exists(@"Setting")) Directory.CreateDirectory(@"Setting");
            setting = ResultViewerSetting.ReadSetting();
            if (String.IsNullOrEmpty(setting.ResultViewerFolserPath)) {
                setting.ResultViewerFolserPath = RESULT_FOLDER;
                setting.WriteSetting();
            }

            // Resultファイルを読み込みます
            var ResultDirectoryInfo = new DirectoryInfo(setting.ResultViewerFolserPath);
            var ResultFiles = ResultDirectoryInfo.GetFiles("*.xml", SearchOption.AllDirectories).ToList();

            foreach (var ResultFile in ResultFiles) {
                Result result = Result.Read(ResultFile.FullName);
                results.Add(result);
            }

            SetDgv();

        }

        private void SetDgv() {
            ChangeDgvMode(true);
            DgvResult.RowCount = 1;
            DgvResult.Columns["LEVEL"].DefaultCellStyle.Format = "N1";
            DgvResult.Columns["Score"].DefaultCellStyle.Format = "N2";
            DgvResult.Columns["Score1P"].DefaultCellStyle.Format = "N2";
            DgvResult.Columns["Score2P"].DefaultCellStyle.Format = "N2";
            foreach (var result in results.Select((v, i) => (v, i))) {
                DgvResult.RowCount = result.i + 1;
                DgvResult["Title", result.i].Value              = result.v.Title;
                DgvResult["LEVEL", result.i].Value              = result.v.Level;
                if(result.v.ResultType == ResultType.NOTCLEAR) {
                    DgvResult["ClearMode", result.i].Value = "FAILED";
                } else {
                    DgvResult["ClearMode", result.i].Value = result.v.ResultType.ToString();
                }
                DgvResult["Great1P", result.i].Value            = result.v.GreatP1;
                DgvResult["Good1P", result.i].Value             = result.v.GoodP1;
                DgvResult["Bad1P", result.i].Value              = result.v.BadP1;
                DgvResult["Great2P", result.i].Value            = result.v.GreatP2;
                DgvResult["Good2P", result.i].Value             = result.v.GoodP2;
                DgvResult["Bad2P", result.i].Value              = result.v.BadP2;
                DgvResult["Score", result.i].Value              = result.v.Score;
                DgvResult["EXScore", result.i].Value            = result.v.EXScore;
                DgvResult["MAXMinus", result.i].Value           = result.v.MAXMinus;
                DgvResult["Score1P", result.i].Value            = result.v.ScoreP1;
                DgvResult["Score2P", result.i].Value            = result.v.ScoreP2;

            }
            DgvResult.Columns["Title"].Width                    = 450;
            DgvResult.Columns["LEVEL"].Width                    = 100;
            DgvResult.Columns["ClearMode"].Width                = 150;
            DgvResult.Columns["Great1P"].Width                  = 100;
            DgvResult.Columns["Good1P"].Width                   = 100;
            DgvResult.Columns["Bad1P"].Width                    = 100;
            DgvResult.Columns["Great2P"].Width                  = 100;
            DgvResult.Columns["Good2P"].Width                   = 100;
            DgvResult.Columns["Bad2P"].Width                    = 100;
            DgvResult.Columns["Score"].Width                    = 100;
            DgvResult.Columns["EXScore"].Width                  = 100;
            DgvResult.Columns["MAXMinus"].Width                 = 100;
            DgvResult.Columns["Score1P"].Width                  = 100;
            DgvResult.Columns["Score2P"].Width                  = 100;

            DgvResult.Columns["LEVEL"].Frozen = true;
        }

        private void SetResult() {
            // Resultファイルを読み込みます
            var ResultDirectoryInfo = new DirectoryInfo(setting.ResultViewerFolserPath);
            var ResultFiles = ResultDirectoryInfo.GetFiles("*.xml", SearchOption.AllDirectories).ToList();
            results = new List<Result>();

            foreach (var ResultFile in ResultFiles) {
                Result result = Result.Read(ResultFile.FullName);
                results.Add(result);
            }
            SetDgv();
        }

        private void ChangeDgvMode(bool IsStart) {
            if (IsStart) {
                DgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                DgvResult.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            } else {
                DgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                DgvResult.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
        }

        private void BtFolderSetting_Click(object sender, EventArgs e) {
            var folderBrowserDialog = new CommonOpenFileDialog();
            folderBrowserDialog.InitialDirectory = setting.ResultViewerFolserPath;
            folderBrowserDialog.IsFolderPicker = true;

            if(folderBrowserDialog.ShowDialog() == CommonFileDialogResult.Ok) {
                setting.ResultViewerFolserPath = folderBrowserDialog.FileName;
                setting.WriteSetting();
                SetResult();
            }
        }
    }
}
