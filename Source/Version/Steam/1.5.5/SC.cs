using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

public class SC
{
	private static readonly char[] Dictionary = "aAbcdEFgGijJklmnoOpPqrSUwXyZ234BCDfHIKMNQtuvWxz7@!#_=|}'68~`$%ehLRsTVY0159+{:?^&*()-/[];,<>.\\".ToCharArray();

	private static readonly char[] References = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ0123456789@~`!#$%^&*()_-=+/|[]{}';:,<>?.\\".ToCharArray();

	public static string ExtravagantCheat;

	public static string SystemHooked;

	protected static bool Guard;

	protected static long Ticker;

	protected static Process THIS;

	protected static Process GG;

	private static string Decode(string In)
	{
		char[] array = In.ToCharArray();
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			int num = -1;
			char[] dictionary = Dictionary;
			foreach (char c in dictionary)
			{
				num++;
				if (array[i] == c)
				{
					array[i] = References[num];
					num = -1;
					break;
				}
			}
			text += array[i];
		}
		return text;
	}

	public static string Encode(string In)
	{
		char[] array = In.ToCharArray();
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			int num = -1;
			char[] references = References;
			foreach (char c in references)
			{
				num++;
				if (array[i] == c)
				{
					array[i] = Dictionary[num];
					num = -1;
					break;
				}
			}
			text += array[i];
		}
		return text;
	}

	private static string Decode(string Key, string In)
	{
		char[] array = In.ToCharArray();
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			int num = -1;
			foreach (char c in Key)
			{
				num++;
				if (array[i] == c)
				{
					array[i] = References[num];
					num = -1;
					break;
				}
			}
			text += array[i];
		}
		return text;
	}

	public static string Encode(string Key, string In)
	{
		char[] array = In.ToCharArray();
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			int num = -1;
			char[] references = References;
			foreach (char c in references)
			{
				num++;
				if (array[i] == c)
				{
					array[i] = Key[num];
					num = -1;
					break;
				}
			}
			text += array[i];
		}
		return text;
	}

	public static bool ToBool(string String)
	{
		if (String == null)
		{
			String = "False";
		}
		if (bool.TryParse(Decode(String), out var result))
		{
			return result;
		}
		return false;
	}

	public static string FromBool(bool Value)
	{
		return Encode(Value.ToString());
	}

	public static bool ToBool(string Key, string String)
	{
		if (String == null)
		{
			String = "False";
		}
		if (bool.TryParse(Decode(Key, String), out var result))
		{
			return result;
		}
		return false;
	}

	public static string FromBool(string Key, bool Value)
	{
		return Encode(Key, Value.ToString());
	}

	public static byte ToByte(string String)
	{
		if (byte.TryParse(Decode(String), out var result))
		{
			return result;
		}
		return 0;
	}

	public static string FromByte(byte Byte)
	{
		return Encode(Byte.ToString());
	}

	public static byte ToByte(string Key, string String)
	{
		if (byte.TryParse(Decode(Key, String), out var result))
		{
			return result;
		}
		return 0;
	}

	public static string FromByte(string Key, byte Byte)
	{
		return Encode(Key, Byte.ToString());
	}

	public static double ToDouble(string String)
	{
		if (double.TryParse(Decode(String), out var result))
		{
			return result;
		}
		return 0.0;
	}

	public static string FromDouble(double Double)
	{
		return Encode(Double.ToString());
	}

	public static double ToDouble(string Key, string String)
	{
		if (double.TryParse(Decode(Key, String), out var result))
		{
			return result;
		}
		return 0.0;
	}

	public static string FromDouble(string Key, double Double)
	{
		return Encode(Key, Double.ToString());
	}

	public static float ToFloat(string String)
	{
		if (float.TryParse(Decode(String), out var result))
		{
			return result;
		}
		return 0f;
	}

	public static string FromFloat(float Float)
	{
		return Encode(Float.ToString());
	}

	public static float ToFloat(string Key, string String)
	{
		if (float.TryParse(Decode(Key, String), out var result))
		{
			return result;
		}
		return 0f;
	}

	public static string FromFloat(string Key, float Float)
	{
		return Encode(Key, Float.ToString());
	}

	public static int ToInt(string String)
	{
		if (int.TryParse(Decode(String), out var result))
		{
			return result;
		}
		return 0;
	}

	public static string FromInt(int Int)
	{
		return Encode(Int.ToString());
	}

	public static int ToInt(string Key, string String)
	{
		if (int.TryParse(Decode(Key, String), out var result))
		{
			return result;
		}
		return 0;
	}

	public static string FromInt(string Key, int Int)
	{
		return Encode(Key, Int.ToString());
	}

	public static string ToString(string String)
	{
		return Decode(String);
	}

	public static string FromString(string String)
	{
		return Encode(String);
	}

	public static string ToString(string Key, string String)
	{
		return Decode(Key, String);
	}

	public static string FromString(string Key, string String)
	{
		return Encode(Key, String);
	}

	public static IEnumerator GameGuard(string PATH)
	{
		THIS = Process.GetCurrentProcess();
		try
		{
			Process[] processesByName = Process.GetProcessesByName(THIS.ProcessName);
			if (processesByName.Length > 1)
			{
				THIS.Kill();
			}
		}
		catch
		{
		}
		string CFG = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ozW5oSzGg.cfg");
		string XCFG = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HGS4.cfg");
		Guard = true;
		string GGP = PATH + "/ozW5oSzGg.exe";
		if (!File.Exists(GGP))
		{
			THIS.Kill();
		}
		yield return null;
		GG = new Process();
		GG.StartInfo.FileName = GGP;
		GG.Start();
		GG.Exited += Kill;
		if (File.Exists(XCFG))
		{
			try
			{
				SystemHooked = File.ReadAllText(XCFG);
				File.Delete(XCFG);
			}
			catch
			{
			}
		}
		Ticker = DateTime.Now.Ticks;
		while (Guard)
		{
			if (DateTime.Now.Ticks > Ticker + 50000000)
			{
				Ticker = DateTime.Now.Ticks;
				File.WriteAllText(CFG, THIS.ProcessName);
				try
				{
					if (GG.HasExited)
					{
						GG.Start();
					}
				}
				catch
				{
					THIS.Kill();
				}
				try
				{
					Process[] processesByName2 = Process.GetProcessesByName(GG.ProcessName);
					if (processesByName2.Length < 1)
					{
						THIS.Kill();
					}
				}
				catch
				{
				}
				try
				{
					string directoryName = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
					string path = Directory.GetParent(directoryName).ToString();
					string[] files = Directory.GetFiles(directoryName);
					string[] array = files;
					foreach (string path2 in array)
					{
						FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Path.GetFullPath(path2));
						if (versionInfo.FileName.ToLower(CultureInfo.InvariantCulture).Contains("dx3dx"))
						{
							ExtravagantCheat = FromBool(Value: true);
						}
						if (versionInfo.InternalName.ToLower(CultureInfo.InvariantCulture).Contains("dx3dx"))
						{
							ExtravagantCheat = FromBool(Value: true);
						}
						if (versionInfo.ProductName.ToLower(CultureInfo.InvariantCulture).Contains("dx3dx"))
						{
							ExtravagantCheat = FromBool(Value: true);
						}
					}
					files = Directory.GetFiles(path);
					string[] array2 = files;
					foreach (string path3 in array2)
					{
						FileVersionInfo versionInfo2 = FileVersionInfo.GetVersionInfo(Path.GetFullPath(path3));
						if (versionInfo2.FileName.ToLower(CultureInfo.InvariantCulture).Contains("dx3dx"))
						{
							ExtravagantCheat = FromBool(Value: true);
						}
						if (versionInfo2.InternalName.ToLower(CultureInfo.InvariantCulture).Contains("dx3dx"))
						{
							ExtravagantCheat = FromBool(Value: true);
						}
						if (versionInfo2.ProductName.ToLower(CultureInfo.InvariantCulture).Contains("dx3dx"))
						{
							ExtravagantCheat = FromBool(Value: true);
						}
					}
				}
				catch
				{
				}
			}
			yield return null;
		}
		GG.Kill();
	}

	private static void Kill(object sender, EventArgs e)
	{
		THIS.Kill();
		try
		{
			GG.Start();
		}
		catch
		{
		}
	}

	public static bool HasGameGuard()
	{
		return Guard;
	}

	public static void StopGameGuard()
	{
		try
		{
			GG.Kill();
		}
		catch
		{
		}
	}
}
