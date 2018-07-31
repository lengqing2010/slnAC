using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace IME
{
    public partial class Form1 : Form
    {

        [DllImport("Imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("Imm32.dll")]
        private static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("Imm32.dll")]
        private static extern bool ImmGetConversionStatus(IntPtr hIMC, ref int fdwConversion, ref int fdwSentence);

        [DllImport("Imm32.dll")]
        private static extern bool ImmGetOpenStatus(IntPtr hIMC);

        [DllImport("Imm32.dll")]
        private static extern bool ImmSetOpenStatus(IntPtr hIMC, long fOpen);

        [DllImport("Imm32.dll")]
        private static extern bool ImmSetConversionStatus(IntPtr hIMC, int fdwConversion, int fdwSentence);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // 入力モードを示す定数
        /// <summary>英数字入力モード</summary>
        private const int IME_CMODE_ALPHANUMERIC = 0x0;
        /// <summary>言語依存入力モード</summary>
        private const int IME_CMODE_NATIVE = 0x1;
        /// <summary>日本語入力モード</summary>
        private const int IME_CMODE_JAPANESE = IME_CMODE_NATIVE;
        /// <summary>カタカナ入力モード</summary>
        private const int IME_CMODE_KATAKANA = 0x2;
        /// <summary>言語入力モード</summary>
        private const int IME_CMODE_LANGUAGE = 0x3;
        /// <summary>全角入力モード</summary>
        private const int IME_CMODE_FULLSHAPE = 0x8;
        /// <summary>ローマ字入力モード</summary>
        private const int IME_CMODE_ROMAN = 0x10;

        // 変換モードを示す定数の宣言
        /// <summary>無変換モード</summary>
        private const int IME_SMODE_NONE = 0x0;
        /// <summary>複数文字変換モード</summary>
        private const int IME_SMODE_PLAURALCLAUSE = 0x1;
        /// <summary>単一文字変換モード</summary>
        private const int IME_SMODE_SINGLECONVERT = 0x2;
        /// <summary>自動変換モード</summary>
        private const int IME_SMODE_AUTOMATIC = 0x4;
        /// <summary>予測変換モード</summary>
        private const int IME_SMODE_PHRASEPREDICT = 0x8;


        /// <summary>
        /// IMEモードを切り替える
        /// </summary>
        /// <param name="handle">ウィンドウのハンドル</param>
        /// <param name="imeMode">IMEモード</param>
        private static void ChangeImeMode(IntPtr handle, ImeMode imeMode)
        {
            IntPtr lngInputContextHandle;
            int lngStatusIMEConversion = 0;
            bool lngWin32apiResultCode;

            // ウィンドウに関連付けされた入力コンテキストを取得
            lngInputContextHandle = ImmGetContext(handle);

            if (lngInputContextHandle != new IntPtr(0))
            {
                // IMEをオープン
                lngWin32apiResultCode = ImmSetOpenStatus(lngInputContextHandle, 1);

                // 初期入力モードを指定
                switch (imeMode)
                {
                    case ImeMode.AlphaFull: // 全角英数字
                        lngStatusIMEConversion = IME_CMODE_FULLSHAPE;
                        break;
                    case ImeMode.Hiragana: // 全角ひらがな
                        lngStatusIMEConversion = IME_CMODE_NATIVE | IME_CMODE_FULLSHAPE;
                        break;
                    case ImeMode.Katakana: // 全角カナ
                        lngStatusIMEConversion = IME_CMODE_NATIVE | IME_CMODE_FULLSHAPE | IME_CMODE_KATAKANA;
                        break;
                    case ImeMode.KatakanaHalf: // 半角カナ
                        lngStatusIMEConversion = IME_CMODE_NATIVE | IME_CMODE_KATAKANA;
                        break;
                    default:
                        // 半角英数字、その他
                        lngWin32apiResultCode = ImmSetOpenStatus(lngInputContextHandle, 0);
                        lngWin32apiResultCode = ImmReleaseContext(handle, lngInputContextHandle);
                        return;
                }

                // IMEの初期方式を設定
                lngWin32apiResultCode = ImmSetConversionStatus(lngInputContextHandle, lngStatusIMEConversion, IME_SMODE_NONE);

                // 入力コンテキストを開放
                lngWin32apiResultCode = ImmReleaseContext(handle, lngInputContextHandle);
            }
        }

        // ひらがなにしてみる
        private void textBox2_Enter(object sender, EventArgs e)
        {
            ChangeImeMode(this.Handle, ImeMode.Hiragana);
        }

        // IMEをOFFにする
        private void textBox3_Enter(object sender, EventArgs e)
        {
            ChangeImeMode(this.Handle, ImeMode.NoControl);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ChangeImeMode(this.Handle, ImeMode.Hiragana);
        }

    }
}
