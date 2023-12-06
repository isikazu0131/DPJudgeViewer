using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JiroJudgeViewer {
	internal class Win32Api {
		private const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;

		public const int
			PROCESS_VM_READ = 0x0010,
			PROCESS_VM_WRITE = 0x0020,
			PROCESS_DUP_HANDLE = 0x0040,
			PROCESS_QUERY_INFO = 0x0400,
			PROCESS_QUERY_LIMITED_INFO = 0x1000;

		public const int
			MEM_COMMIT = 0x1000,
			MEM_RESERVE = 0x2000;

		public const int
			MEM_DECOMMIT = 0x4000,
			MEM_RELEASE = 0x8000;

		public const int
			PAGE_NOACCESS = 0x01,
			PAGE_READONLY = 0x02,
			PAGE_READWRITE = 0x04,
			PAGE_WRITECOPY = 0x08;

		[DllImport("kernel32")]
		public static extern IntPtr VirtualAllocEx(
			IntPtr hProcess, IntPtr lpAddress, int dwSize, int flAllocationType, int flProtect);

		[DllImport("kernel32")]
		public static extern int VirtualFreeEx(
			IntPtr hProcess, IntPtr lpAddress, int dwSize, int dwFreeType);

		[DllImport("kernel32")]
		public static extern int WriteProcessMemory(
			IntPtr hProcess, IntPtr lpBaseAddress,
			byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);

		[DllImport("kernel32", SetLastError = true)]
		public static extern bool ReadProcessMemory(
			IntPtr hProcess, IntPtr lpBaseAddress,
			byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);

		/// <summary>
		/// 指定されたプロセスのメモリ領域からデータを読み取ります。
		/// </summary>
		/// <param name="hProcess">プロセスのハンドル</param>
		/// <param name="lpBaseAddress">読み取り開始アドレス</param>
		/// <param name="lpBuffer">データを格納するバッファ</param>
		/// <param name="nSize">読み取りたいバイト数</param>
		/// <param name="lpNumberOfBytesRead">読み取ったバイト数</param>
		/// <returns></returns>
		[DllImport("kernel32")]
		public static extern int ReadProcessMemory(
			IntPtr hProcess, IntPtr lpBaseAddress,
			IntPtr lpBuffer, int nSize, out int lpNumberOfBytesRead);

		[DllImport("kernel32", SetLastError = true)]
		public static extern IntPtr OpenProcess(
			int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		[DllImport("kernel32")]
		public static extern int CloseHandle(IntPtr hObject);

		[DllImport("kernel32.dll")]
		public static extern uint FormatMessage(
			uint dwFlags, IntPtr lpSource,
			uint dwMessageId, uint dwLanguageId,
			StringBuilder lpBuffer, int nSize,
			IntPtr Arguments);

		public static void WriteError(int errCode) {
			StringBuilder message = new StringBuilder(255);
			Win32Api.FormatMessage(
				FORMAT_MESSAGE_FROM_SYSTEM,
				IntPtr.Zero,
				(uint)errCode,
				0,
				message,
				message.Capacity,
				IntPtr.Zero);
			Console.WriteLine("Win32ApiError：" + message.ToString());
		}
	}

}
