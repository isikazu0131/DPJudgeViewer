using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JiroJudgeViewer {

    public class ResultViewerSetting {

        private const string SettingPath = @"Setting\ResultViewerSetting.xml";

        /// <summary>
        /// リザルト閲覧画面で使用するフォルダパス
        /// </summary>
        public string ResultViewerFolserPath;

        public ResultViewerSetting(string resultViewerFolserPath) { 
            ResultViewerFolserPath = resultViewerFolserPath;
        }

        private ResultViewerSetting() {

        }

        /// <summary>
        /// 設定ファイル読み込み
        /// </summary>
        /// <param name="settingPath"></param>
        /// <returns></returns>
        static public ResultViewerSetting ReadSetting() {
            if (!File.Exists(SettingPath)) { return new ResultViewerSetting(); }

            XmlSerializer settingXML = new XmlSerializer(typeof(ResultViewerSetting));
            ResultViewerSetting setting = new ResultViewerSetting();

            using (StreamReader sr = new StreamReader(SettingPath)) {
                setting = (ResultViewerSetting)settingXML.Deserialize(sr);
            }

            return setting;
        }

        /// <summary>
        /// 設定ファイル書き込み
        /// </summary>
        /// <param name="settingPath"></param>
        /// <param name="editorPath"></param>
        public void WriteSetting() {
            ResultViewerSetting setting = new ResultViewerSetting(ResultViewerFolserPath);

            XmlSerializer settingXML = new XmlSerializer(typeof(ResultViewerSetting));

            using (StreamWriter sw = new StreamWriter(SettingPath)) {
                settingXML.Serialize(sw, setting);
            }
        }

    }
}
