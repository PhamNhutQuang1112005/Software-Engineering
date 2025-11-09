using System;
using System.IO;
using DAL;

namespace BLL
{
    public static class BLL_TapKetQua
    {
        public static void LogPdfExport(string donHangID, string fullPath)
        {
            if (string.IsNullOrWhiteSpace(donHangID) || string.IsNullOrWhiteSpace(fullPath))
                return;

            string ketQuaID = Guid.NewGuid().ToString("N");
            string fileName = Path.GetFileName(fullPath);
            DAL_TapKetQua.ThemTapKetQua(
                ketQuaID,
                donHangID,
                fileName,
                fullPath,
                "PDF",
                generatedBy: null,
                trangThaiXuat: "OK"
            );
        }
    }
}
