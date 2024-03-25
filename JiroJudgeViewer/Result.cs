using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;

namespace JiroJudgeViewer {

    [DataContract]
    public class Result {
        /// <summary>
        /// 曲名
        /// </summary>
        [DataMember]
        public string Title;

        /// <summary>
        /// レベル
        /// </summary>
        [DataMember]
        public double Level;

        /// <summary>
        /// 良(P1)
        /// </summary>
        [DataMember]
        public int GreatP1;

        /// <summary>
        /// 可(P1)
        /// </summary>
        [DataMember]
        public int GoodP1;

        /// <summary>
        /// 不可(P1)
        /// </summary>
        [DataMember]
        public int BadP1;

        /// <summary>
        /// スコア(P1)
        /// </summary>
        [DataMember]
        public double ScoreP1;

        /// <summary>
        /// 良(P2)
        /// </summary>
        [DataMember]
        public int GreatP2;

        /// <summary>
        /// 可(P2)
        /// </summary>
        [DataMember]
        public int GoodP2;

        /// <summary>
        /// 不可(P2)
        /// </summary>
        [DataMember]
        public int BadP2;

        /// <summary>
        /// スコア(P2)
        /// </summary>
        [DataMember]
        public double ScoreP2;

        /// <summary>
        /// 総合スコア
        /// </summary>
        [DataMember]
        public double Score;

        /// <summary>
        /// ExScore
        /// </summary>
        [DataMember]
        public int EXScore;

        /// <summary>
        /// 理論値からいくら離れているか
        /// </summary>
        [DataMember]
        public int MAXMinus;

        /// <summary>
        /// リザルト種類
        /// </summary>
        [DataMember]
        public ResultType ResultType;

        private Result() {
            ResultType = ResultType.NOTCLEAR;
        }

        public Result(string title, double level, int greatP1, int goodP1, int badP1, double scoreP1, int greatP2, int goodP2, int badP2, double scoreP2, double score, int eXScore, int maxMinus, ResultType resultType){
            Title = title;
            Level = level;
            GreatP1 = greatP1;
            GoodP1 = goodP1;
            BadP1 = badP1;
            ScoreP1 = scoreP1;
            GreatP2 = greatP2;
            GoodP2 = goodP2;
            BadP2 = badP2;
            ScoreP2 = scoreP2;
            Score = score;
            EXScore = eXScore;
            MAXMinus = maxMinus;
            ResultType = resultType;
        }

        static public Result Read(string Path) {
            var result = new Result();
            if (!File.Exists(Path)) return null;

            var serializer = new DataContractSerializer(typeof(Result));

            using (var reader = XmlReader.Create(Path)) {
                result = (Result)serializer.ReadObject(reader);
            }

            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(Result));
            //using(StreamReader sr = new StreamReader(Path)) {
            //    result = (Result)xmlSerializer.Deserialize(sr);
            //}
            return result;
        }

        static public bool Write(Result result, string FolderPath, string ResultFileName) {
            if (!Directory.Exists(FolderPath)) {
                Directory.CreateDirectory(FolderPath);
            }
            // 既にrecoldファイルがあった場合に自己べかどうか見る
            bool IsNewRecord = true;
            bool IsStatusUpdate = true;
            string ResultPath = Path.Combine(FolderPath, ResultFileName);
            Result OldResult = new Result();

            if (File.Exists(ResultPath)) {
                OldResult = Read(ResultPath);
                if(OldResult.Score >= result.Score) IsNewRecord = false;
                if(OldResult.ResultType >= result.ResultType) IsStatusUpdate = false;
            }

            // 記録、ステータス両方更新
            if (IsNewRecord && IsStatusUpdate) {
                var serializer = new DataContractSerializer(typeof(Result));

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                using (XmlWriter xw = XmlWriter.Create(ResultPath, settings)) { 
                    serializer.WriteObject(xw, result);
                }
                return true;
            // 記録のみを更新
            } else if(IsNewRecord && !IsStatusUpdate) {
                result.ResultType = OldResult.ResultType;
                var serializer = new DataContractSerializer(typeof(Result));

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                using (XmlWriter xw = XmlWriter.Create(ResultPath, settings)) {
                    serializer.WriteObject(xw, result);
                }
                return true;
            //ステータスのみを更新
            }else if(!IsNewRecord && IsStatusUpdate){
                OldResult.ResultType = result.ResultType;
                var serializer = new DataContractSerializer(typeof(Result));

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                using (XmlWriter xw = XmlWriter.Create(ResultPath, settings)) {
                    serializer.WriteObject(xw, OldResult);
                }
                return true;
            }

            return false;
        }
    }

    public enum ResultType {
        NOPLAY,     // 未プレイ
        NOTCLEAR,   // 未クリア
        CLEAR,      // クリア
        GAUGEMAX,   // 魂ゲージMAX
        HARDCLEAR,  // ハードクリア
        FULLCOMBO,  // フルコンボ
        ALLPERFECT  // 全良
    }
}
