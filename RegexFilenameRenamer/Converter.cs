using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ambiesoft.RegexFilenameRenamer
{
    class Converter
    {
        public enum CONVERT_TYPE {
            REG,
            HIRAKATA,
        };
        private CONVERT_TYPE convertType_;

        public enum HIRAKATA_CONVERT_TYPE
        {
            TO_HANKAKU,
            TO_ZENKAKU,
            TO_HIRAGANA,
            TO_KATAKANA,
        };
        private HIRAKATA_CONVERT_TYPE hirakataConvertType_;

        public bool Init(string strRegFind, string strRegTarget, bool bRegIgnoreCase)
        {
            this.strRegFind_ = strRegFind;
            this.strRegTarget_ = strRegTarget;
            this.bRegIgnoreCase_ = bRegIgnoreCase;
            this.convertType_ = CONVERT_TYPE.REG;

            try
            {
                if (bRegIgnoreCase)
                    reg_ = new Regex(strRegFind, RegexOptions.IgnoreCase);
                else
                    reg_ = new Regex(strRegFind);
            }
            catch (Exception ex)
            {
                CppUtils.CenteredMessageBox(ex.Message);
                return false;
            }
            return true;
        }
        public bool Init(HIRAKATA_CONVERT_TYPE hirakataConvertType)
        {
            this.convertType_ = CONVERT_TYPE.HIRAKATA;
            this.hirakataConvertType_ = hirakataConvertType;
            return true;
        } 
        string strRegFind_;
        string strRegTarget_;
        bool bRegIgnoreCase_;
        Regex reg_;

        public string Replace(string orgFileName)
        {
            if (convertType_ == CONVERT_TYPE.REG)
            {
                return reg_.Replace(orgFileName, strRegTarget_);
            }
            else if(convertType_ == CONVERT_TYPE.HIRAKATA)
            {
                switch (hirakataConvertType_)
                {
                    case HIRAKATA_CONVERT_TYPE.TO_HANKAKU:
                        return Umayadia.Kana.KanaConverter.ToNarrow(orgFileName);
                    case HIRAKATA_CONVERT_TYPE.TO_ZENKAKU:
                        return Umayadia.Kana.KanaConverter.ToWide(orgFileName);
                    case HIRAKATA_CONVERT_TYPE.TO_HIRAGANA:
                        return Umayadia.Kana.KanaConverter.ToHiragana(orgFileName);
                    case HIRAKATA_CONVERT_TYPE.TO_KATAKANA:
                        return Umayadia.Kana.KanaConverter.ToKatakana(orgFileName);
                }
            }
            throw new Exception("TODO");
        }
    }
}
