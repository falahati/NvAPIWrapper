using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NvAPICodeGenerator.Generator
{
    internal class FriendlyNameRuleSet
    {
        public FriendlyNameRuleSet() : this(null)
        {
        }

        public FriendlyNameRuleSet(
            Dictionary<string, string> replaceRules = null,
            string[] removeRules = null,
            string[] fixedCaseWords = null)
        {
            ReplaceRules = new Dictionary<string, string>(replaceRules ?? new Dictionary<string, string>());
            RemoveRules = new List<string>(removeRules ?? new string[0]);
            FixedCaseWords = new List<string>(fixedCaseWords ?? new string[0]);
        }

        public List<string> FixedCaseWords { get; }
        public List<string> RemoveRules { get; }
        public Dictionary<string, string> ReplaceRules { get; }

        public string Apply(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            str = str.Trim();

            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;

            var separators = new[] {'_', ' ', '-', '|'};

            var strParts = str.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany(s =>
                {
                    var replace = ReplaceRules.FirstOrDefault(
                        pair => pair.Key.Equals(s, StringComparison.InvariantCultureIgnoreCase)
                    );

                    if (!string.IsNullOrWhiteSpace(replace.Value))
                    {
                        return replace.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    }

                    return new[] {s};
                })
                .Where(s => RemoveRules.All(rr => !rr.Equals(s, StringComparison.InvariantCultureIgnoreCase)))
                .Select(s =>
                    {
                        var fixedCase = FixedCaseWords.FirstOrDefault(
                            uw => uw.Equals(s, StringComparison.InvariantCultureIgnoreCase)
                        );

                        if (!string.IsNullOrWhiteSpace(fixedCase))
                        {
                            return fixedCase;
                        }

                        if (s.Length == 1)
                        {
                            return textInfo.ToUpper(s);
                        }

                        return textInfo.ToTitleCase(textInfo.ToLower(s));
                    }
                )
                .ToArray();

            return string.Join("", strParts);
        }
    }
}