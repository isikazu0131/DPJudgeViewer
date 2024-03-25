using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UmeboshiLibrary;
using System.Xml.Serialization;

namespace JiroJudgeViewer {
    /// <summary>
    /// tjaファイルに関するクラス
    /// </summary>
    public class TJA {

        // 譜面の外部情報
        public string TITLE;
        public string SUBTITLE;
        public string WAVE;
        public double OFFSET;
        public double DEMOSTART;
        public double BPM;
        public double SONGVOL;
        public double SEVOL;
        public DIFFICULTY COURSE;
        public double LEVEL;
        public double TOTAL;
        public int SCOREMODE;
        public BMSCROLL BMSCROLL;
        public STYLE STYLE = STYLE.SP;

        /// <summary>
        /// 加算スコア初期値
        /// </summary>
        public double SCOREINIT;

        /// <summary>
        /// 加算スコアの加算値
        /// </summary>
        public double SCOREDIFF;

        // 譜面の内部情報
        /// <summary>
        /// TJAのパス
        /// </summary>
        public FileInfo TJAPath;

        /// <summary>
        /// ノーツ情報
        /// </summary>
        public List<Note> Notes;

        /// <summary>
        /// ノーツ情報（P1）
        /// </summary>
        public List<Note> NotesListP1;

        /// <summary>
        /// ノーツ情報（P2）
        /// </summary>
        public List<Note> NotesListP2;

        /// <summary>
        /// 小節情報
        /// </summary>
        public List<Measure> Measures;

        /// <summary>
        /// ノーツ数
        /// </summary>
        public int NotesCount;

        /// <summary>
        /// ノーツ数（P1）
        /// </summary>
        public int NotesCountP1;

        /// <summary>
        /// ノーツ数（P2）
        /// </summary>
        public int NotesCountP2;

        /// <summary>
        /// 最高BPM
        /// </summary>
        public double MaxBPM;

        /// <summary>
        /// 最低BPM
        /// </summary>
        public double MinBPM;

        ///// <summary>
        ///// 音源再生時間
        ///// </summary>
        //public double MusicPlayTime;

        ///// <summary>
        ///// 譜面再生時間
        ///// </summary>
        //public double TJAPlayTime;

        // コンストラクタ
        public TJA() {

        }

        static public List<TJA> GetTJAs(DirectoryInfo directoryInfo) {
            var tja_infos = directoryInfo.GetFiles("*.tja", SearchOption.AllDirectories);
            List<TJA> TJAs = new List<TJA>();
            foreach (var tja in tja_infos) {
                TJAs.Add(new TJA(tja));
            }
            return TJAs;
        }

        public TJA(FileInfo TjaFileInfo) {
            try {
                TJAPath = TjaFileInfo;
                Notes = new List<Note>();
                NotesListP1 = new List<Note>();
                NotesListP2 = new List<Note>();
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); // memo: Shift-JISを扱うためのおまじない

                var Contents = File.ReadAllLines(TjaFileInfo.FullName, Encoding.GetEncoding("Shift_JIS"));
                this.BMSCROLL = BMSCROLL.NONE;
                this.COURSE = DIFFICULTY.ONI;

                // 抽出した文字列を一時格納
                string extracted_data;
                foreach (var line in Contents) {

                    string processedline = DeleteComment(line).Trim(); // 余計なコメントを削除します

                    if (processedline.StartsWith("TITLE:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "TITLE:");
                        TITLE = extracted_data;
                    }
                    if (processedline.StartsWith("SUBTITLE:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "SUBTITLE:");
                        SUBTITLE = extracted_data;
                    }
                    if (processedline.StartsWith("WAVE:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "WAVE:");
                        WAVE = extracted_data;
                    }
                    if (processedline.StartsWith("OFFSET:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "OFFSET:");
                        extracted_data = DataToNumString(extracted_data);
                        if (double.TryParse(extracted_data, out double extracted_data_converted) == false) {
                            OFFSET = 0;
                            continue;
                        }
                        OFFSET = extracted_data_converted;
                    }
                    if (processedline.StartsWith("DEMOSTART:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "DEMOSTART:");
                        extracted_data = DataToNumString(extracted_data);
                        if (String.IsNullOrEmpty(extracted_data) || double.TryParse(extracted_data, out double extracted_data_converted) == false) {
                            DEMOSTART = 0;
                            continue;
                        }
                        DEMOSTART = extracted_data_converted;
                    }
                    if (processedline.StartsWith("BPM:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "BPM:").Trim();
                        extracted_data = System.Text.RegularExpressions.Regex.Matches(extracted_data, @"[0-9]+\.?[0-9]*")[0].Value;
                        BPM = double.Parse(extracted_data);
                        MinBPM = BPM;
                        MaxBPM = BPM;
                    }
                    if (processedline.StartsWith("SONGVOL:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "SONGVOL:");
                        extracted_data = DataToNumString(extracted_data);
                        if (String.IsNullOrEmpty(extracted_data) || double.TryParse(extracted_data, out double extracted_data_converted) == false) {
                            SONGVOL = 100;
                            continue;
                        }
                        SONGVOL = double.Parse(extracted_data);
                    }
                    if (processedline.StartsWith("SEVOL:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "SEVOL:");
                        extracted_data = DataToNumString(extracted_data);
                        if (String.IsNullOrEmpty(extracted_data) || double.TryParse(extracted_data, out double extracted_data_converted) == false) {
                            SEVOL = 100;
                            continue;
                        }
                        SEVOL = double.Parse(UmeboshiString.CutToEnd(extracted_data, "SEVOL:"));
                    }
                    if (processedline.StartsWith("TOTAL:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "TOTAL:");
                        extracted_data = DataToNumString(extracted_data);
                        if (String.IsNullOrEmpty(extracted_data) || double.TryParse(extracted_data, out double extracted_data_converted) == false) {
                            TOTAL = 0;
                            continue;
                        }
                        TOTAL = double.Parse(UmeboshiString.CutToEnd(extracted_data, "TOTAL:"));
                    }
                    if (processedline.StartsWith("COURSE:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "COURSE:");
                        COURSE = GetCourse(extracted_data);
                    }
                    if (processedline.StartsWith("#BPMCHANGE")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "#BPMCHANGE");
                        if (String.IsNullOrEmpty(extracted_data) || double.TryParse(extracted_data, out double ChangedBPM) == false) {
                            continue;
                        }
                        if (MaxBPM < ChangedBPM) MaxBPM = ChangedBPM;
                        if (MinBPM > ChangedBPM) MinBPM = ChangedBPM;
                    }
                    if (processedline.StartsWith("LEVEL:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "LEVEL:");
                        extracted_data = DataToNumString(extracted_data);
                        if (String.IsNullOrEmpty(extracted_data) || double.TryParse(extracted_data, out double extracted_data_converted) == false) {
                            LEVEL = 0;
                            continue;
                        }
                        LEVEL = extracted_data_converted;
                    }
                    if (processedline.StartsWith("STYLE:")) {
                        extracted_data = UmeboshiString.CutToEnd(processedline, "STYLE:");
                        extracted_data = DataToNumString(extracted_data);
                        if (String.IsNullOrEmpty(extracted_data) || int.TryParse(extracted_data, out int extracted_data_converted) == false) {
                            STYLE = STYLE.SP;
                            continue;
                        }
                        switch (extracted_data_converted) {
                            case 1:
                                STYLE = STYLE.SP;
                                break;
                            case 2:
                                STYLE = STYLE.DP;
                                break;
                            default:
                                STYLE = STYLE.SP;
                                break;
                        };
                    }
                    if (processedline.StartsWith("#BMSCROLL")) {
                        BMSCROLL = BMSCROLL.BMSCROLL;
                    }
                    if (processedline.StartsWith("#HBSCROLL")) {
                        BMSCROLL = BMSCROLL.HBSCROLL;
                    }
                }

                if (STYLE == STYLE.SP) {
                    NotesCount = GetNotes(Contents, "");
                } else if (STYLE == STYLE.DP) {
                    NotesCountP1 = GetNotes(Contents, "P1");
                    NotesCountP2 = GetNotes(Contents, "P2");
                    NotesCount = NotesCountP1 + NotesCountP2;
                }

                //MusicPlayTime = GetOggFullTime(Path.Combine(TjaFileInfo.DirectoryName, WAVE));
                //TJAPlayTime = GetTJAPlayTime(Contents, MusicPlayTime, OFFSET, BPM, NotesCount);

                if (SUBTITLE == null) SUBTITLE = "";
            }
            catch (Exception ex) {
                MessageBox.Show(TjaFileInfo.FullName);
            }

        }

        static public string CourseToString(DIFFICULTY diff) {
            switch (diff) {
                case DIFFICULTY.EASY:
                    return "かんたん";
                case DIFFICULTY.NORMAL:
                    return "ふつう";
                case DIFFICULTY.HARD:
                    return "むずかしい";
                case DIFFICULTY.ONI:
                    return "おに";
                case DIFFICULTY.EDIT:
                    return "edit";
                default:
                    return "そのほか";
            }
        }

        /// <summary>
        /// 数値情報に変換します
        /// </summary>
        private string DataToNumString(string data) {
            if (System.Text.RegularExpressions.Regex.Matches(data, @"[0-9]+\.?[0-9]*").Count == 0) {
                return "";
            }
            return System.Text.RegularExpressions.Regex.Matches(data, @"[0-9]+\.?[0-9]*")[0].Value;
        }

        private DIFFICULTY GetCourse(string coursetxt) {
            if (int.TryParse(coursetxt, out var coursevalue)) {
                switch (coursevalue) {
                    case 0: return DIFFICULTY.EASY;
                    case 1: return DIFFICULTY.NORMAL;
                    case 2: return DIFFICULTY.HARD;
                    case 3: return DIFFICULTY.ONI;
                    case 4: return DIFFICULTY.EDIT;
                    default: return DIFFICULTY.ONI;
                }
            } else {
                switch (coursetxt) {
                    case "Easy": return DIFFICULTY.EASY;
                    case "Normal": return DIFFICULTY.NORMAL;
                    case "Hard": return DIFFICULTY.HARD;
                    case "Oni": return DIFFICULTY.ONI;
                    case "Edit": return DIFFICULTY.EDIT;
                    default: return DIFFICULTY.ONI;
                }
            }
        }

        /// <summary>
        /// ノーツ数をカウント、音符の取得をします
        /// </summary>
        /// <param name="Contains"></param>
        /// <returns></returns>
        private int GetNotes(string[] Contents, string PlaySide) {
            int count = 0; // 現在の行数取得
            int combo = 0; // コンボ数
            bool isTJA = false;
            bool isDP = false;
            bool isGOGO = false;
            string DeleteCommentContent; // コメントを削除した行
            while (count < Contents.Length) {

                if (Contents[count].StartsWith("#START")) {

                    // DPかつ指定した側の譜面じゃない場合スキップ
                    if (PlaySide != "" && Contents[count].Contains(PlaySide) != true) {
                        count++;
                        continue;
                    }
                    count++;
                    isTJA = true;
                    break;
                }
                count++;
            }
            if (isTJA == false) return 0;
            for (int i = count; i < Contents.Length; i++) {
                DeleteCommentContent = DeleteComment(Contents[i]);
                if (DeleteCommentContent.StartsWith("#MEASURE") ||
                    DeleteCommentContent.StartsWith("#BPMCHANGE") ||
                    DeleteCommentContent.StartsWith("#SCROLL") ||
                    DeleteCommentContent.StartsWith("#DELAY") ||
                    DeleteCommentContent.StartsWith("//")) continue;
                if (DeleteCommentContent.StartsWith("#GOGOSTART")) {
                    isGOGO = true;
                }
                if (DeleteCommentContent.StartsWith("#GOGOEND")) {
                    isGOGO = false;
                }

                // 「1,2,3,4」の個数を計測
                for (int j = 0; j < Contents[i].Length; j++) {
                    if (Contents[i][j] == '1' || Contents[i][j] == '2' || Contents[i][j] == '3' || Contents[i][j] == '4') {
                        if (PlaySide == "P1") {
                            NotesListP1.Add(new Note(int.Parse(Contents[i][j].ToString()), combo, isGOGO));
                        } else if (PlaySide == "P2") {
                            NotesListP2.Add(new Note(int.Parse(Contents[i][j].ToString()), combo, isGOGO));
                        } else {
                            Notes.Add(new Note(int.Parse(Contents[i][j].ToString()), combo, isGOGO));
                        }
                        combo++;
                    }
                }

                if (Contents[i].StartsWith("#END")) break;
            }

            return combo;
        }

        ///// <summary>
        ///// 小節情報を登録し、譜面再生時間を計算します
        ///// </summary>
        ///// <returns></returns>
        //double GetTJAPlayTime(string[] Contents, double MusicTime, double offset, double NowBPM, int NotesCount) {
        //    try {
        //        if (NotesCount <= 1) return 0;

        //        // 現在の経過時間
        //        double result = 0;
        //        // 拍子情報
        //        double NowBunbo = 4, NowBunshi = 4;
        //        int count = 0; // 現在の行数取得
        //        int combo = 0; // コンボ数
        //        bool isTJA = false;
        //        bool isOvered = false;
        //        bool isTJAStarted = false;

        //        string DeleteCommentContent; // コメントを削除した行
        //        while (count < Contents.Length) {

        //            if (Contents[count].StartsWith("#START")) {
        //                isTJA = true;
        //                break;

        //            }
        //            count++;
        //        }
        //        if (isTJA == false) return 0;

        //        List<List<string>> measures = new List<List<string>>();
        //        List<string> measuretmp = new List<string>();
        //        for (int i = count; i < Contents.Length; i++) {

        //            DeleteCommentContent = DeleteComment(Contents[i]);

        //            if (Contents[i].StartsWith("#START")) continue;
        //            if (Contents[i].StartsWith("#END")) break;
        //            if (!DeleteCommentContent.EndsWith(',')) {
        //                measuretmp.Add(DeleteCommentContent);
        //            } else {
        //                measuretmp.Add(DeleteCommentContent);
        //                measures.Add(measuretmp);
        //                measuretmp = new List<string>();
        //            }

        //        }

        //        foreach (var measure in measures) {
        //            if (measure.Count == 1) {
        //                if (measure[0].StartsWith("#BPMCHANGE")) {
        //                    var bpm = UmeboshiString.CutToEnd(measure[0], "#BPMCHANGE").Trim();
        //                    if (double.TryParse(bpm, out NowBPM)) continue;
        //                } else if (measure[0].StartsWith("#MEASURE")) {
        //                    var measureStr = UmeboshiString.CutToEnd(measure[0], "#MEASURE").Trim();
        //                    var bunboStr = UmeboshiString.CutToStart(measureStr, '/');
        //                    var bunshiStr = UmeboshiString.CutToEnd(measureStr, '/');

        //                    if (!double.TryParse(bunboStr, out NowBunbo)) continue;
        //                    if (!double.TryParse(bunshiStr, out NowBunshi)) continue;
        //                } else if (measure[0].StartsWith("#DELAY")) {
        //                    var delaystr = UmeboshiString.CutToEnd(measure[0], "#DELAY").Trim();
        //                    if (double.TryParse(delaystr, out double delay)) continue;
        //                    result += delay;
        //                } else if (measure[0].EndsWith(",")) {
        //                    // if (NowBPM < 0 && NowBunshi > 0 || NowBPM > 0 && NowBunshi < 0) return -1;
        //                    // 1ノーツ目がない小節はスルー
        //                    if (!isTJAStarted && !measure[0].Contains('1') && !measure[0].Contains('2') && !measure[0].Contains('3') && !measure[0].Contains('4')) continue;
        //                    isTJAStarted = true;
        //                    result += (60 / NowBPM) * (NowBunbo / NowBunshi) * 4;
        //                    if (result >= MusicTime) {
        //                        isOvered = true;
        //                        // 音符を後ろから見ていき、音源の長さを越えないギリギリかつ音符のある位置を特定する（音源再生内の最後のノーツの位置を確認する）
        //                        for (int i = measure[0].Length - 1; i > -1; i--) {
        //                            if (measure[0].Length == 1) result -= 1 / 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                            else result -= 1 / (measure[0].Length - 1) * 60 / NowBPM * 4 * NowBunbo / NowBunshi;

        //                            if (result >= MusicTime) continue;
        //                            if (measure[0][i] == '1' ||
        //                                measure[0][i] == '2' ||
        //                                measure[0][i] == '3' ||
        //                                measure[0][i] == '4') {
        //                                break;
        //                            }
        //                        }
        //                        break;
        //                    }
        //                }

        //                // 小節情報が複数行にわたるとんでもない譜面、やめてくれ～～～～（ウエストランド井口）
        //            } else {
        //                // 小節内に音符がいくつあるかを数える（何分音符の小節か）
        //                int notesCount = 0;
        //                foreach (var line in measure) {
        //                    if (!line.StartsWith("#BPMCHANGE") &&
        //                       !line.StartsWith("#MEASURE") &&
        //                       !line.StartsWith("#DELAY") &&
        //                       !line.StartsWith("#SCROLL")) {
        //                        foreach (var chr in line) {
        //                            if (chr == '0' ||
        //                               chr == '1' ||
        //                               chr == '2' ||
        //                               chr == '3' ||
        //                               chr == '4' ||
        //                               chr == '5' ||
        //                               chr == '6' ||
        //                               chr == '7' ||
        //                               chr == '8' ||
        //                               chr == '9') notesCount++;
        //                        }
        //                    }
        //                }

        //                foreach (var line in measure) {
        //                    if (line.StartsWith("#BPMCHANGE")) {
        //                        var bpm = UmeboshiString.CutToEnd(line, "#BPMCHANGE").Trim();
        //                        if (double.TryParse(bpm, out NowBPM)) continue;
        //                    } else if (line.StartsWith("#MEASURE")) {
        //                        var measureStr = UmeboshiString.CutToEnd(line, "#MEASURE").Trim();
        //                        var bunboStr = UmeboshiString.CutToStart(measureStr, '/');
        //                        var bunshiStr = UmeboshiString.CutToEnd(measureStr, '/');
        //                        if (bunshiStr.EndsWith('q')) bunshiStr = bunshiStr.Replace("q", "");

        //                        if (!double.TryParse(bunboStr, out NowBunbo)) continue;
        //                        if (!double.TryParse(bunshiStr, out NowBunshi)) continue;
        //                    } else if (line.StartsWith("#DELAY")) {
        //                        var delaystr = UmeboshiString.CutToEnd(line, "#DELAY").Trim();
        //                        if (double.TryParse(delaystr, out double delay)) continue;
        //                        result += delay;
        //                    } else if (line.EndsWith(",")) {
        //                        // 1ノーツ目がない小節はスルー
        //                        if (NowBPM < 0 && NowBunshi > 0 || NowBPM > 0 && NowBunshi < 0) return -1;

        //                        if (!isTJAStarted && !measure[0].Contains('1') && !measure[0].Contains('2') && !measure[0].Contains('3') && !measure[0].Contains('4')) continue;
        //                        isTJAStarted = true;
        //                        if (notesCount > 0) result += (line.Length - 1) / (double)notesCount * 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                        else result += 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                        if (result >= MusicTime) {
        //                            isOvered = true;
        //                            // 音符を後ろから見ていき、音源の長さを越えないギリギリかつ音符のある位置を特定する（音源再生内の最後のノーツの位置を確認する）
        //                            for (int i = line.Length - 2; i > -1; i--) {
        //                                result -= 1 / (double)notesCount * 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                                if (result + offset >= MusicTime) continue;
        //                                if (line[i] == '1' ||
        //                                    line[i] == '2' ||
        //                                    line[i] == '3' ||
        //                                    line[i] == '4') {
        //                                    break;
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    } else if (!string.IsNullOrEmpty(line) && !line.StartsWith("#GOGOSTART") && !line.StartsWith("#GOGOEND") && !line.StartsWith("#SCROLL") && !line.StartsWith("#BARLINE")) {
        //                        if (notesCount > 0) {
        //                            if (line.EndsWith(",")) result += (line.Length - 1) / (double)notesCount * 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                            else result += line.Length / (double)notesCount * 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                        } else result += 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                        if (result >= MusicTime) {
        //                            isOvered = true;
        //                            // 音符を後ろから見ていき、音源の長さを越えないギリギリかつ音符のある位置を特定する（音源再生内の最後のノーツの位置を確認する）
        //                            for (int i = line.Length - 2; i > -1; i--) {
        //                                if (line.StartsWith("#SCROLL") ||
        //                                    line.StartsWith("#BPMCHANGE") ||
        //                                    line.StartsWith("#DELAY") ||
        //                                    line.StartsWith("#MEASURE") ||
        //                                    line.StartsWith("#BARLINEON") ||
        //                                    line.StartsWith("#BARLINEOFF") ||
        //                                    line.StartsWith("#GOGOSTART") ||
        //                                    line.StartsWith("#GOGOEND")

        //                                    ) continue;
        //                                result -= 1 / (double)notesCount * 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                                if (result <= MusicTime) continue;
        //                                if (line[i] == '1' ||
        //                                    line[i] == '2' ||
        //                                    line[i] == '3' ||
        //                                    line[i] == '4') {
        //                                    break;
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            if (isOvered) break;
        //        }

        //        if (isOvered == false) {
        //            if (measures[measures.Count - 1].Count > 1) {
        //                int notesCount = 0;
        //                string noteline = measures[measures.Count - 1][measures[measures.Count - 1].Count - 1];
        //                foreach (var line in measures[measures.Count - 1]) {
        //                    if (!line.StartsWith("#BPMCHANGE") &&
        //                       !line.StartsWith("#MEASURE") &&
        //                       !line.StartsWith("#DELAY") &&
        //                       !line.StartsWith("#SCROLL")) {
        //                        foreach (var chr in line) {
        //                            if (chr == '0' ||
        //                               chr == '1' ||
        //                               chr == '2' ||
        //                               chr == '3' ||
        //                               chr == '4' ||
        //                               chr == '5' ||
        //                               chr == '6' ||
        //                               chr == '7' ||
        //                               chr == '8' ||
        //                               chr == '9') notesCount++;
        //                        }
        //                    }
        //                }
        //                // 音符を後ろから見ていき、音源の長さを越えないギリギリかつ音符のある位置を特定する（音源再生内の最後のノーツの位置を確認する）
        //                for (int i = noteline.Length - 1; i > -1; i--) {

        //                    if (result + offset >= MusicTime) continue;
        //                    if (noteline[i] == '1' ||
        //                        noteline[i] == '2' ||
        //                        noteline[i] == '3' ||
        //                        noteline[i] == '4') {
        //                        break;
        //                    }
        //                    if (notesCount > 0) result -= 1 / (double)notesCount * 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                    else result -= 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                }
        //            } else {
        //                string noteline = measures[measures.Count - 1][measures[measures.Count - 1].Count - 1];
        //                // 音符を後ろから見ていき、音源の長さを越えないギリギリかつ音符のある位置を特定する（音源再生内の最後のノーツの位置を確認する）
        //                for (int i = noteline.Length - 2; i > -1; i--) {
        //                    if (result + offset >= MusicTime) continue;
        //                    if (noteline[i] == '1' ||
        //                        noteline[i] == '2' ||
        //                        noteline[i] == '3' ||
        //                        noteline[i] == '4') {
        //                        break;
        //                    }
        //                    if (noteline.Length > 1) result -= 1 / (double)(noteline.Length - 1) * 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                    else result -= 1 / 60 / NowBPM * 4 * NowBunbo / NowBunshi;
        //                }
        //            }

        //        }

        //        return result;

        //    }
        //    catch (Exception ex) {
        //        // MessageBox.Show(ex.Message);
        //        return 0;
        //    }

        //}


        /// <summary>
        /// 余計なコメントを削除します
        /// </summary>
        /// <param name="line">読み込まれた行</param>
        /// <returns></returns>
        private string DeleteComment(string line) {
            if (line.Contains("//") == false) return line;
            else if (line.StartsWith("//")) return line;
            try {
                return UmeboshiString.CutToStart(line, "//");
            }
            catch {
                return "";
            }

        }

        ///// <summary>
        ///// 音源ファイルの長さを取得します
        ///// </summary>
        ///// <param name="oggPath"></param>
        ///// <returns></returns>
        //private double GetOggFullTime(string oggPath) {
        //    if (!File.Exists(oggPath)) return 0;
        //    VorbisReader reader = new VorbisReader(oggPath);
        //    TimeSpan timeSpan = reader.TotalTime;
        //    reader.Dispose();
        //    return timeSpan.TotalSeconds;
        //}

    }

    public class Note {
        public NoteKind noteKind;
        public Fingering fingering;
        public double timing;
        public bool IsGOGO;

        public Note(int kind, int count, bool isGOGO) {
            noteKind = (NoteKind)kind;
            IsGOGO = isGOGO;
            if (count % 2 == 0) {
                fingering = Fingering.Right;
            } else {
                fingering = Fingering.Left;
            }
        }

        public Note(int kind) {
            noteKind = (NoteKind)kind;
        }

        public enum Fingering {
            Left,
            Right
        }

        public enum NoteKind {
            None,
            Dong,
            Ka,
            DongL,
            KaL,
            Roll,
            RollL,
            Balloon,
            RollEnd,
            Poteto
        }
    }

    /// <summary>
    /// 各小節の内部情報
    /// </summary>
    public class Measure {

        /// <summary>
        /// 現在のBPM
        /// </summary>
        public double BPM;

        /// <summary>
        /// 拍子の分母部分
        /// </summary>
        public double Measure_den;

        /// <summary>
        /// 拍子の分子部分
        /// </summary>
        public double Measure_mol;

        /// <summary>
        /// 小節内のノーツ情報
        /// </summary>
        public List<Note> Notes;

        /// <summary>
        /// 1小節の1行を参照
        /// </summary>
        /// <param name="Content"></param>
        public Measure(string Content, double den, double mol) {
            Notes = new List<Note>();
            Measure_den = den;
            Measure_mol = mol;
            foreach (var note in Content) {
                Notes.Add(new Note(int.Parse(note.ToString())));
            }
        }

        //    /// <summary>
        //    /// 1小節あたりの時間を計算
        //    /// </summary>
        //    /// <param name="MeasureString"></param>
        //    /// <returns></returns>
        //    public double CalcMeasureTime(List<string> MeasureString, double BPM, double den, double mol) {
        //        // 現在のノーツ数
        //        int notecount = 0;

        //        // 経過時間
        //        double elapsed = 0;

        //        foreach(var line in MeasureString) {
        //            if (line.StartsWith("#MEASURE")) {
        //                continue;
        //            }
        //            if (line.StartsWith("#BPMCHANGE")) {
        //                if(MeasureRow.Length != 0) {

        //                }
        //                NowBPM = double.Parse(UmeboshiString.CutToEnd(line, "#BPMCHNGE"));
        //                continue;

        //            }
        //            if (line.StartsWith("#DELAY")) {
        //                elapsed += double.Parse(UmeboshiString.CutToEnd(line, "#DELAY"));
        //                continue;

        //            }
        //            if (line.StartsWith("#SCROLL")) {
        //                continue;
        //            }

        //            if (line.EndsWith(",")) {
        //                // 1行で書かれた1小節
        //                if(notecount == 0) {
        //                    elapsed = 60 / BPM * 4 * (mol / den);
        //                }
        //            }
        //        }

        //        return elapsed
        //    }

        //}

    }

    public enum DIFFICULTY {
        EASY,
        NORMAL,
        HARD,
        ONI,
        EDIT
    }

    public enum BMSCROLL {
        NONE,
        BMSCROLL,
        HBSCROLL
    }

    public enum STYLE {
        SP,
        DP
    }
}
