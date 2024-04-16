using System.Collections.Generic;

namespace LivingMessiah.Web.Features.Admin.Video.Models;

/*
| SwId | DateYMD    | Torah        | VideoId     | FileName                                |
| ---- | ---------- | ------------ | ----------- | --------------------------------------- |
| 001  | 2021-10-02 | Gen-01-01-19 | YBrQpgXjaSg | 001_2021-10-02_Gen-01-01-19_YBrQpgXjaSg |


			//new string("001" + "2021-10-02" + "Gen-01-01-19"+ "YBrQpgXjaSg")

			
public void Dump() 
{
	foreach (var item in VideoFactory.PopulateVideoList)
	{

	}
}

Severity	Code	Description	Project	File	Line	Suppression State
Error	CS0446	Foreach cannot operate on a 'method group'. Did you intend to invoke the 'method group'?	LivingMessiah.Web	C:\Users\JohnM\source\repos\LivingMessiahBlazor\LivingMessiah.Web\Features\Admin\Video\Models\VideoImage.cs	34	Active


*/

public record VideoImage(string VideoId, string FileName);

public static class VideoFactory
{
	//static string?  videoId = "UF5u7-PrSys";
	//static string?  imageUrl = $"http://img.youtube.com/vi/{videoId}/maxresdefault.jpg";

	public static void Dump()
	{
		foreach (var item in VideoFactory.PopulateVideoList())
		{

		}
	}

	public static List<VideoImage> PopulateVideoList()
	{
		var x = new List<VideoImage>
		{
			new VideoImage("u7eTS5x4-G0", "128_2024-03-09_u7eTS5x4-G0_Num-35-9-36-13"),
			new VideoImage("it58SG4mOT0", "127_2024-03-02_it58SG4mOT0_Num-34-1-35-8"),
			new VideoImage("idxNQV6O4IM", "126_2024-02-24_idxNQV6O4IM_Num-33"),
			new VideoImage("snyUxPLltyw", "125_2024-02-17_snyUxPLltyw_Num-32"),
			new VideoImage("QEjbIbSrGFo", "124_2024-02-10_QEjbIbSrGFo_Num-30-and-31"),
			new VideoImage("jAXGzExAHA0", "123_2024-02-03_jAXGzExAHA0_Num-28-and-29"),
			new VideoImage("57ikqL83vpA", "122_2024-01-27_57ikqL83vpA_Num-26-52-27-23"),
			new VideoImage("O5WNnUlsWpU", "121_2024-01-20_O5WNnUlsWpU_Num-25-10-26-51"),
			new VideoImage("k8royd5vhMA", "120_2024-01-13_k8royd5vhMA_Num-23-2-25-9"),
			new VideoImage("u3OYEKRF64s", "119_2024-01-06_u3OYEKRF64s_Num-22-2-23-1"),
			new VideoImage("29PuDL2-tCM", "118_2023-12-30_29PuDL2-tCM_Num-20-14-22-1"),
			new VideoImage("CA4IsQjR6TE", "117_2023-12-23_CA4IsQjR6TE_Num-19-1-20-13"),
			new VideoImage("LPnI3iBMXHA", "116_2023-12-16_LPnI3iBMXHA_Num-17-and-18"),
			new VideoImage("GhmC5JIGA60", "115_2023-12-09_GhmC5JIGA60_Num-16"),
			new VideoImage("ARBZu99P7q0", "114_2023-12-02_ARBZu99P7q0_Num-15"),
			new VideoImage("1crDrsF3bfo", "113_2023-11-25_1crDrsF3bfo_Num-14"),
			new VideoImage("mcPARU1Q9eM", "112_2023-11-18_mcPARU1Q9eM_Num-13"),
			new VideoImage("HMgNUQl2_0U", "111_2023-11-11_HMgNUQl2_0U_Num-12"),
			new VideoImage("cTGmikiCHBA", "110_2023-11-04_cTGmikiCHBA_Num-11"),
			new VideoImage("TIUQVYiKVu4", "109_2023-10-28_TIUQVYiKVu4_Num-10"),
			new VideoImage("nMlems58Cp0", "108_2023-10-21_nMlems58Cp0_Num-8-and-9"),
			new VideoImage("qWaURsv7wCk", "107_2023-10-14_qWaURsv7wCk_Num-6-22-7-89"),
			new VideoImage("plpT6Z79NzI", "104_2023-09-23_plpT6Z79NzI_Num-4-21-5-10"),
			new VideoImage("iLDi7DcHap8", "103_2023-09-16_iLDi7DcHap8_Num-3-14-4-20"),
			new VideoImage("UXTe1Lxc1uo", "102_2023-09-09_UXTe1Lxc1uo_Num-2-14-3-13"),
			new VideoImage("BLc7cjIhCEY", "101_2023-09-02_BLc7cjIhCEY_Num-1-1-2-13"),
			new VideoImage("QG2oc24NwW4", "100_2023-08-26_QG2oc24NwW4_Lev-26-3-27-34"),
			new VideoImage("4f_YbpTffTk", "099_2023-08-19_4f_YbpTffTk_Lev-25-39-26-2"),
			new VideoImage("2UcUNYNTYBo", "098_2023-08-12_2UcUNYNTYBo_Lev-25-1-38"),
			new VideoImage("b1V6jS9WTvo", "097_2023-08-05_b1V6jS9WTvo_Lev-24"),
			new VideoImage("OFI6QXJLO-s", "096_2023-07-29_OFI6QXJLO-s_Lev-22-and-23"),
			new VideoImage("KufKnhSyg7c", "095_2023-07-22_KufKnhSyg7c_Lev-21"),
			new VideoImage("RD8fhWFE1Vo", "094_2023-07-15_RD8fhWFE1Vo_Lev-19-and-20"),
			new VideoImage("brF8sEFmWQA", "093_2023-07-08_brF8sEFmWQA_Lev-18"),
			new VideoImage("bm8Qm-mFQBU", "092_2023-07-01_bm8Qm-mFQBU_Lev-17"),
			new VideoImage("McNuzGek4Fc", "091_2023-06-24_McNuzGek4Fc_Lev-16"),
			new VideoImage("HmiXNR88dtc", "090_2023-06-17_HmiXNR88dtc_Lev-15"),
			new VideoImage("EvRDjl9xLXU", "089_2023-06-10_EvRDjl9xLXU_Lev-14"),
			new VideoImage("sGOrMMoTXTQ", "088_2023-06-03_sGOrMMoTXTQ_Lev-13-29-59"),
			new VideoImage("BuaE_TiZL0U", "087_2023-05-27_BuaE_TiZL0U_Lev-12-1-13-28"),
			new VideoImage("wptZkDuEGE4", "086_2023-05-20_wptZkDuEGE4_Lev-9---11"),
			new VideoImage("TxUKbH7XqFg", "085_2023-05-13_TxUKbH7XqFg_Lev-8"),
			new VideoImage("KGEH8oXyz9s", "084_2023-05-06_KGEH8oXyz9s_Lev-7"),
			new VideoImage("MI5ukWuemA0", "082_2023-04-22_MI5ukWuemA0_Lev-5-1-6-7"),
			new VideoImage("z4Bu0QuB_kM", "081_2023-04-15_z4Bu0QuB_kM_Lev-4"),
			new VideoImage("zGFt21HfPFY", "080_2023-04-08_zGFt21HfPFY_Lev-3"),
			new VideoImage("3ez8V6vRhIM", "079_2023-04-01_3ez8V6vRhIM_Lev-1-and-2"),
			new VideoImage("kJvTtHXrqMU", "078_2023-03-25_kJvTtHXrqMU_Exo-39-1-40-38"),
			new VideoImage("irtEIt2vWrs", "077_2023-03-18_irtEIt2vWrs_Exo-38-21-31"),
			new VideoImage("rqGzSvPHIVI", "076_2023-03-11_rqGzSvPHIVI_Exo-37-1-38-20"),
			new VideoImage("bjahp7ZBxJU", "075_2023-03-04_bjahp7ZBxJU_Exo-34-27-36-38"),
			new VideoImage("wzB_5hRNuUM", "074_2023-02-25_wzB_5hRNuUM_Exo-32-14-34-26"),
			new VideoImage("5Y7qQ4AJ1GQ", "073_2023-02-18_5Y7qQ4AJ1GQ_Exo-31-1-32-13"),
			new VideoImage("RzyJ88fzxoo", "072_2023-02-11_RzyJ88fzxoo_Exo-30-11-38"),
			new VideoImage("rmfsJ4EE7ak", "071_2023-02-04_rmfsJ4EE7ak_Exo-30-1-10"),
			new VideoImage("reXToIdCF9M", "070_2023-01-28_reXToIdCF9M_Exo-29"),
			new VideoImage("773ax-8pPbs", "069_2023-01-21_773ax-8pPbs_Exo-27-20-28-43"),
			new VideoImage("AVw2uFp9n-A", "068_2023-01-14_AVw2uFp9n-A_Exo-27-1-19"),
			new VideoImage("MCZhjNMj5Iw", "067_2023-01-07_MCZhjNMj5Iw_Exo-25-1-26-37"),
			new VideoImage("dK123ZBIUZo", "066_2022-12-31_dK123ZBIUZo_Exo-24"),
			new VideoImage("nGc6OhMaA7Q", "065_2022-12-24_nGc6OhMaA7Q_Exo-22-25-23-33"),
			new VideoImage("gavcvzQHy2I", "064_2022-12-17_gavcvzQHy2I_Exo-21-1-22-24"),
			new VideoImage("8LO_GBQbvAs", "063_2022-12-10_8LO_GBQbvAs_Exo-19-7-20-26"),
			new VideoImage("nxp8lrW_puo", "062_2022-12-03_nxp8lrW_puo_Exo-18-1-19-6"),
			new VideoImage("5f7lk3A1g8M", "061_2022-11-26_5f7lk3A1g8M_Exo-16-25-17-16"),
			new VideoImage("5f7lk3A1g8M", "060_2022-11-19_5f7lk3A1g8M_Exo-15-22-16-24"),
			new VideoImage("zgCaWnyBsBc", "059_2022-11-12_zgCaWnyBsBc_Exo-13-21-15-21"),
			new VideoImage("62wqpQlZU6U", "058_2022-11-05_62wqpQlZU6U_Exo-13-1-20"),
			new VideoImage("0D03wqIZrrM", "057_2022-10-29_0D03wqIZrrM_Exo-12-29-51"),
			new VideoImage("1qLyoq6LcMQ", "056_2022-10-22_1qLyoq6LcMQ_Exo-11-1-12-28"),
			new VideoImage("bRRx-fALLtw", "054_2022-10-08_bRRx-fALLtw_Exo-8-16-9-35"),
			new VideoImage("nO0qfyqaLmw", "053_2022-10-01_nO0qfyqaLmw_Exo-7-8-8-15"),
			new VideoImage("EnVGxKTbGrs", "052_2022-09-24_EnVGxKTbGrs_Exo-6-2-7-7"),
			new VideoImage("toZ0Rc6DBL0", "051_2022-09-17_toZ0Rc6DBL0_Exo-4-14-6-1"),
			new VideoImage("V3zEE84u0hA", "050_2022-09-10_V3zEE84u0hA_Exo-3-1-4-13"),
			new VideoImage("m_UaAPDiAF8", "049_2022-09-03_m_UaAPDiAF8_Exo-1-and-2"),
			new VideoImage("b7kwJfe44pE", "048_2022-08-27_b7kwJfe44pE_Gen-49-28-50-26"),
			new VideoImage("J2ixFHseuY0", "047_2022-08-20_J2ixFHseuY0_Gen-48-1-49-27"),
			new VideoImage("68qmW-0PR3c", "046_2022-08-13_68qmW-0PR3c_Gen-46-28-47-31"),
			new VideoImage("J6_m3J_JwKk", "045_2022-08-06_J6_m3J_JwKk_Gen-44-18-46-27"),
			new VideoImage("_5XQ6w2Hol0", "044_2022-07-30__5XQ6w2Hol0_Gen-43-24-44-17"),
			new VideoImage("O9vYxCWD16Y", "043_2022-07-23_O9vYxCWD16Y_Gen-42-18-43-23"),
			new VideoImage("IJGU0lpyI_E", "042_2022-07-16_IJGU0lpyI_E_Gen-41-38-42-17"),
			new VideoImage("erak36kuMJc", "041_2022-07-09_erak36kuMJc_Gen-41-1-37"),
			new VideoImage("k0okaUkikN4", "040_2022-07-02_k0okaUkikN4_Gen-40"),
			new VideoImage("thjf4AbXSM4", "039_2022-06-25_thjf4AbXSM4_Gen-39"),
			new VideoImage("C0m0NluwV24", "038_2022-06-18_C0m0NluwV24_Gen-38"),
			new VideoImage("2B26YZH2gxA", "037_2022-06-11_2B26YZH2gxA_Gen-37"),
			new VideoImage("ALKB9fiS9VM", "036_2022-06-04_ALKB9fiS9VM_Gen-35-9-36-43"),
			new VideoImage("DWypN3ySN78", "035_2022-05-28_DWypN3ySN78_Gen-33-18-35-8"),
			new VideoImage("EoGvDtuHc64", "034_2022-05-21_EoGvDtuHc64_Gen-32-4-33-17"),
			new VideoImage("-ZL_W6geROM", "033_2022-05-14_-ZL_W6geROM_Gen-31-3-32-3"),
			new VideoImage("YqTlxIko190", "032_2022-05-07_YqTlxIko190_Gen-30-22-31-2"),
			new VideoImage("cjmQiab4nqQ", "031_2022-04-30_cjmQiab4nqQ_Gen-29-31-30-21"),
			new VideoImage("V_05aextWI4", "030_2022-04-23_V_05aextWI4_Gen-28-10-29-30"),
			new VideoImage("ryCtOYcngqE", "029_2022-04-16_ryCtOYcngqE_Gen-27-30-28-9"),
			new VideoImage("GZHS99Jk5Bo", "028_2022-04-09_GZHS99Jk5Bo_Gen-27-1-29"),
			new VideoImage("l8vFCGdpiK0", "027_2022-04-02_l8vFCGdpiK0_Gen-26-12-35"),
			new VideoImage("Q3orVTLJClI", "026_2022-03-26_Q3orVTLJClI_Gen-25-19-26-11"),
			new VideoImage("ZTbcxSyzaO4", "025_2022-03-19_ZTbcxSyzaO4_Gen-25-1-18"),
			new VideoImage("RQg0DZMuB8A", "024_2022-03-12_RQg0DZMuB8A_Gen-24-42-67"),
			new VideoImage("X_wanVq0J4s", "023_2022-03-05_X_wanVq0J4s_Gen-24-1-41"),
			new VideoImage("BBcT4nluUbI", "022_2022-02-26_BBcT4nluUbI_Gen-23"),
			new VideoImage("IL0s_DeDK64", "021_2022-02-19_IL0s_DeDK64_Gen-22"),
			new VideoImage("NttEbc60098", "020_2022-02-12_NttEbc60098_Gen-21"),
			new VideoImage("_Ie4X6wRPRw", "019_2022-02-05__Ie4X6wRPRw_Gen-20"),
			new VideoImage("YHgvwK6hQ3Q", "018_2022-01-29_YHgvwK6hQ3Q_Gen-19"),
			new VideoImage("aguH72PU6pY", "017_2022-01-22_aguH72PU6pY_Gen-18"),
			new VideoImage("Y8SDj7Wlf8M", "016_2022-01-15_Y8SDj7Wlf8M_Gen-17"),
			new VideoImage("cFqhw67gzU8", "015_2022-01-08_cFqhw67gzU8_Gen-16"),
			new VideoImage("Z78PkQiABzY", "014_2022-01-01_Z78PkQiABzY_Gen-15"),
			new VideoImage("cI28HIsd_jM", "013_2021-12-25_cI28HIsd_jM_Gen-14"),
			new VideoImage("S3wUFTd-TkM", "012_2021-12-18_S3wUFTd-TkM_Gen-12-and-13"),
			new VideoImage("Y8Y6OoSKTMI", "011_2021-12-11_Y8Y6OoSKTMI_Gen-11"),
			new VideoImage("Uez59PFdyeQ", "010_2021-12-04_Uez59PFdyeQ_Gen-9-18-10-32"),
			new VideoImage("PYQul9ap45Q", "009_2021-11-27_PYQul9ap45Q_Gen-8-15-9-17"),
			new VideoImage("noqyTDAB05U", "008_2021-11-20_noqyTDAB05U_Gen-8-1-14"),
			new VideoImage("ZNbrPRpVZUU", "007_2021-11-13_ZNbrPRpVZUU_Gen-6-9-7-24"),
			new VideoImage("n2kjLNrejII", "006_2021-11-06_n2kjLNrejII_Gen-5-1-6-8"),
			new VideoImage("hBuCebHSVQA", "005_2021-10-30_hBuCebHSVQA_Gen-3-24-4-26"),
			new VideoImage("rWuVPbSaCSw", "002_2021-10-09_rWuVPbSaCSw_Gen-1-20-2-3"),
			new VideoImage("YBrQpgXjaSg", "001_2021-10-02_YBrQpgXjaSg_Gen-1-1-19"),
		};
		return x;
	}
}
