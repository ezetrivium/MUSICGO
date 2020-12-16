using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class FileUtils
    {
        public static byte[] GetImageBytes(string path)
        {
            return File.ReadAllBytes(Path.Combine(GlobalValues.FileImagesData, path));
        }

        public static string GetRepoImagePath(string fileName)
        {
            return Path.Combine(GlobalValues.FileImagesData, fileName);
        }

        public static string GetRepoMusicPath(string fileName)
        {
            return Path.Combine(GlobalValues.FileMusicData, fileName);
        }

        public static void ConvertMp3ToWav(string _inPath_,string _outPath)
        {
            using (Mp3FileReader reader = new Mp3FileReader(_inPath_))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(reader))
                {
                    WaveFileWriter.CreateWaveFile(_outPath, pcm);
                }
            }

        }

        public static string GetStandardImagePath()
        {
            return GlobalValues.ImageStandard;
        }

        public static void DeleteImageFile(string fileName)
        {
            try
            {
                string _path = Path.Combine(GlobalValues.FileImagesData, fileName);
                if (File.Exists(_path) && fileName != GlobalValues.ImageStandard)
                {
                    File.Delete(_path);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }
    }
}
