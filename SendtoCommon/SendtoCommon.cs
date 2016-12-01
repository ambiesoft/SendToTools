using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace SendtoCommon
{
    public static class CommonFunction
    {
        public static string getAllArgs(int start)
        {
            return getAllArgs(Environment.CommandLine, start);
        }
        public static string getAllArgs(string cmdline, int start)
        {
            return getAllArgs(cmdline, start, false);
        }
        public static string getAllArgs(string cmdline, int start, bool bGetFirst)
        {
            cmdline = cmdline.TrimStart();
            string org = cmdline;

            char dq = '\0';
            int phase = 0;
            string result = string.Empty;
            int i = 0;
            for (i = 0; i < cmdline.Length; ++i)
            {
                char c = cmdline[i];
                if (phase == 0)
                {
                    if (c == '"' || c == '\'')
                    {
                        dq = c;
                    }
                    else
                    {
                        result += c;
                    }
                    phase = 1;
                }
                else if (phase == 1)
                {
                    if (dq != '\0')
                    {
                        if (dq == c)
                        {
                            break;
                        }
                        else
                        {
                            result += c;
                            continue;
                        }
                    }
                    else
                    {
                        if (char.IsWhiteSpace(c))
                        {
                            break;
                        }
                        else
                        {
                            result += c;
                            continue;
                        }
                    }
                }
            }

            string remain = string.Empty;
            if (cmdline.Length > i)
            {
                remain = cmdline.Substring(i + 1);
            }
            remain = remain.TrimStart();

            if (bGetFirst)
            {
                if (start <= 0)
                {
                    return result;
                }

                string prev = string.Empty;
                if (dq != '\0')
                {
                    prev += dq;
                    prev += result;
                    prev += dq;
                }
                else
                {
                    prev = result;
                }

                return prev + " " + getAllArgs(remain, start - 1, bGetFirst);
            }
            else
            {
                if (start <= 1)
                {
                    return remain;
                }

                return getAllArgs(remain, start - 1);
            }
        }

        public static string undq(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (s[0] == '"' || s[0] == '\'')
            {
                s = s.Trim(s[0]);
            }

            return s;
        }

    }
}