using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NvAPICodeGenerator.Parser;
using NvAPICodeGenerator.Parser.Models;
using NvAPICodeGenerator.Generator;

namespace NvAPICodeGenerator
{
    internal class DriverSettingsTranslator
    {
        private static readonly string[] FixedCaseStrings =
        {
            "3AFR", "3D", "AFR", "AFR2", "API", "BB", "BPP", "CE", "CI",
            "CPU", "CT", "D3D", "DAC", "DAC0", "DAC1", "DIM", "DIV", "DLP", "DP",
            "DRM", "DT", "DWM", "DX10", "DX9", "EH", "FOS", "FPS", "FXAA",
            "GL", "GM10X", "GPU", "IGPU", "LOD", "MCCOMPAT", "MCSFR",
            "MS", "MV", "NV17", "OEM", "OpenGL", "PCIe", "QM", "RGBA", "RT",
            "SFR", "SL", "SLI", "TMON", "UInt", "ULMB", "VAB", "VCAA", "VCE",
            "VR", "VRR", "WKS", "ICafe", "CUDA", "VSync", "GPUs"
        };

        private static readonly Dictionary<string, string> ReplaceRules = new Dictionary<string, string>
        {
            {"AA", "ANTI_ALIASING"},
            {"AF", "ANISOTROPIC"},
            {"ALPHATOCOVERAGE", "ALPHA_TO_COVERAGE"},
            {"ANISO", "ANISOTROPIC"},
            {"AO", "AMBIENT_OCCLUSION"},
            {"APP", "APPLICATION"},
            {"AUTOSELECT", "AUTO_SELECT"},
            {"AVG", "AVERAGE"},
            {"BILINEAR", "BI_LINEAR"},
            {"CAPS", "CAPABILITIES"},
            {"CHECKERBOARD", "CHECKER_BOARD"},
            {"COND", "CONDITION"},
            {"CPL", "CONTROL_PANEL"},
            {"D3DOGL", "D3D_OPENGL"},
            {"DEFAULTPOWER", "DEFAULT_POWER"},
            {"DISALLOW", "DIS_ALLOW"},
            {"DYN", "DYNAMIC"},
            {"EHSHELL", "EH_SHELL"},
            {"ENV", "ENVIRONMENT"},
            {"FLIPINTERVAL", "FLIP_INTERVAL"},
            {"FLIPINTERVAL2", "FLIP_INTERVAL_2"},
            {"FLIPINTERVAL3", "FLIP_INTERVAL_3"},
            {"FLIPINTERVAL4", "FLIP_INTERVAL_4"},
            {"FORCEOFF", "FORCE_OFF"},
            {"FORCEON", "FORCE_ON"},
            {"FULLSCREEN", "FULL_SCREEN"},
            {"GAMMACORRECTION", "GAMMA_CORRECTION"},
            {"H", "HORIZONTAL"},
            {"HIGHPERFORMANCE", "HIGH_PERFORMANCE"},
            {"HIGHQUALITY", "HIGH_QUALITY"},
            {"HPERF", "HIGH_PERFORMANCE"},
            {"HQUAL", "HIGH_QUALITY"},
            {"HW", "HARDWARE"},
            {"INBAND", "IN_BAND"},
            {"LODBIAS", "LOD_BIAS"},
            {"LODBIASADJUST", "LOD_BIAS_ADJUST"},
            {"MAX", "MAXIMUM"},
            {"MAXRES", "MAX_RESOLUTION"},
            {"MCSFRSHOWSPLIT", "MCSFR_SHOW_SPLIT"},
            {"MIN", "MINIMUM"},
            {"MIXEDSAMPLE", "MIXED_SAMPLE"},
            {"MSHYBRID", "MS_HYBRID"},
            {"MULTISAMPLING", "MULTI_SAMPLING"},
            {"NEG", "NEGATIVE"},
            {"NOTSUPPORTED", "NOT_SUPPORTED"},
            {"NUM", "NUMBER"},
            {"NV", "NVIDIA"},
            {"OGL", "OPENGL"},
            {"OPT", "OPTIMAL"},
            {"OPTS2", "OPTIMIZATION"},
            {"PERF", "PERFORMANCE"},
            {"PHYSXINDICATOR", "PHYSX_INDICATOR"},
            {"PRERENDERLIMIT", "PRE_RENDER_LIMIT"},
            {"PS", "PERFORMANCE_STATE"},
            {"PSTATE", "PERFORMANCE_STATE"},
            {"QUAL", "QUALITY"},
            {"RES", "RESOLUTION"},
            {"RR", "REFRESH_RATE"},
            {"SCANLINES", "SCAN_LINES"},
            {"SCANOUT", "SCAN_OUT"},
            {"SEEFRONT", "SEE_FRONT"},
            {"SHADERDISKCACHE", "SHADER_DISK_CACHE"},
            {"SLIAA", "SLI_ANTI_ALIASING"},
            {"SUPERSAMPLE", "SUPER_SAMPLE"},
            {"SUPERVCAA", "SUPER_VCAA"},
            {"SW", "SOFTWARE"},
            {"TEXFILTER", "TEXTURE_FILTERING"},
            {"TIMEOUT", "TIME_OUT"},
            {"TRILIN", "TRILINEAR"},
            {"V", "VERTICAL"},
            {"VAR", "VARIABLE"},
            {"VRPRERENDERLIMIT", "VR_PRE_RENDER_LIMIT"},
            {"VRRFEATUREINDICATOR", "VRR_FEATURE_INDICATOR"},
            {"VRROVERLAYINDICATOR", "VRR_OVERLAY_INDICATOR"},
            {"VRRREQUESTSTATE", "VRR_REQUEST_STATE"},
            {"VSYNCMODE", "V_SYNC_MODE"},
            {"VSYNCSMOOTHAFR", "V_SYNC_SMOOTH_AFR"},
            {"VSYNCTEARCONTROL", "V_SYNC_TEAR_CONTROL"},
            {"VSYNCVRRCONTROL", "V_SYNC_VRR_CONTROL"},
            {"WHITELISTED", "WHITE_LISTED"},
            {"BACKDEPTH", "BACK_DEPTH"},
            {"UPSCALING", "UP_SCALING"},
            {"MAXAA", "MAXIMUM_ANTI_ALIASING"},
            {"FRAMERATE", "FRAME_RATE"},
            {"CTRL", "CONTROL"},
            {"MULTISAMPLE", "MULTI_SAMPLE"}
        };

        private readonly FriendlyNameRuleSet _nameRuleSet;
        private readonly string _nameSpace;
        private readonly DirectoryInfo _readDirectory;
        private readonly DirectoryInfo _writeDirectory;

        public DriverSettingsTranslator(
            DirectoryInfo readDirectory,
            DirectoryInfo writeDirectory,
            string nameSpace = null)
        {
            _readDirectory = readDirectory;
            _writeDirectory = writeDirectory;
            _nameRuleSet = new FriendlyNameRuleSet(ReplaceRules, null, FixedCaseStrings);
            _nameSpace = nameSpace;
        }
        
        // ReSharper disable once ExcessiveIndentation
        public CodeGeneratorResult Generate()
        {
            var driverSettingsDefinitions = _readDirectory
                .GetFiles("NvApiDriverSettings.h", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            var filesDeleted = new List<FileInfo>();
            var filesAdded = new List<FileInfo>();

            if (driverSettingsDefinitions?.Exists != true)
            {
                throw new FileNotFoundException("NvApiDriverSettings.h file not found.");
            }

            var parser = new CHeaderFileParser(driverSettingsDefinitions);
            parser.Parse();

            var settings = parser.Root.SubTrees
                .FirstOrDefault(tree =>
                    tree is CEnum e &&
                    e.Name.Equals("ESetting", StringComparison.InvariantCultureIgnoreCase)
                )
                ?.SubTrees
                .Where(value => value.Name.EndsWith("_ID", StringComparison.InvariantCultureIgnoreCase))
                .OfType<CEnumValue>()
                .Select(
                    value =>
                    {
                        var stringId = value.Name.Substring(0, value.Name.Length - "_ID".Length);
                        var description = GetDescription(stringId, parser.Root.SubTrees);
                        var possibleValues = GetPossibleValues(
                            stringId,
                            parser.Root.SubTrees,
                            out var isCustomPossibleValues
                        );
                        var integerId = value.ResolveValue() as long?;

                        return new
                        {
                            StringId = stringId,
                            Description = description,
                            IntegerId = integerId.HasValue ? (uint) integerId.Value : (uint?) null,
                            PossibleValues = possibleValues,
                            IsCustomPossibleValues = isCustomPossibleValues
                        };
                    }
                )
                .ToArray();

            if (settings == null || settings.Length == 0)
            {
                throw new InvalidOperationException(
                    "ESetting enum is not supported with the NvApiDriverSettings.h file.");
            }

            var _valuesWriteDirectory = new DirectoryInfo(Path.Combine(_writeDirectory.FullName, "SettingValues"));

            if (!_valuesWriteDirectory.Exists)
            {
                _valuesWriteDirectory.Create();
            }
            else
            {
                foreach (var file in _valuesWriteDirectory.EnumerateFiles())
                {
                    file.Delete();
                    filesDeleted.Add(file);
                }
            }

            var settingsEnumValues = new Dictionary<string, uint>();
            var settingsEnumDescriptions = new Dictionary<string, string>();

            foreach (var setting in settings)
            {
                var friendlyName = _nameRuleSet.Apply(setting.StringId);

                var possibleValues = setting.PossibleValues
                    .ToDictionary(pair =>
                    {
                        var name = pair.Key;

                        if (name.StartsWith(setting.StringId + "_"))
                        {
                            name = name.Substring(setting.StringId.Length + 1);
                        }
                        
                        return name.Trim().Trim('_');
                    }, pair => pair.Value).ToArray();

                if (setting.IntegerId != null)
                {
                    settingsEnumValues.Add(setting.StringId, setting.IntegerId.Value);

                    if (!string.IsNullOrWhiteSpace(setting.Description))
                    {
                        settingsEnumDescriptions.Add(setting.StringId, setting.Description);
                    }
                }

                if (possibleValues.Any())
                {
                    CodeGeneratorBase generator;

                    if (setting.IsCustomPossibleValues)
                    {
                        generator = new ConstEnumGenerator(
                            _valuesWriteDirectory,
                            friendlyName,
                            possibleValues.ToDictionary(pair => pair.Key, pair => pair.Value),
                            _nameRuleSet,
                            _nameSpace + ".SettingValues"
                        );
                    }
                    else
                    {
                        generator = new EnumGenerator<uint>(
                            _valuesWriteDirectory,
                            friendlyName,
                            possibleValues
                                .Where(pair => pair.Value is long)
                                .ToDictionary(pair => pair.Key, pair => (uint) (long) pair.Value),
                            _nameRuleSet,
                            _nameSpace + ".SettingValues"
                        );
                    }

                    var possibleValuesEnumGeneratorResult = generator.Generate();

                    foreach (var fileInfo in possibleValuesEnumGeneratorResult.FilesRemoved)
                    {
                        filesDeleted.Add(fileInfo);
                    }

                    foreach (var fileInfo in possibleValuesEnumGeneratorResult.FilesAdded)
                    {
                        filesAdded.Add(fileInfo);
                    }
                }
            }


            var enumGenerator = new EnumGenerator<uint>(
                _writeDirectory,
                "KnownSettingId",
                settingsEnumValues,
                _nameRuleSet,
                _nameSpace,
                settingsEnumDescriptions
            );
            var enumGeneratorResult = enumGenerator.Generate();

            foreach (var fileInfo in enumGeneratorResult.FilesRemoved)
            {
                filesDeleted.Add(fileInfo);
            }

            foreach (var fileInfo in enumGeneratorResult.FilesAdded)
            {
                filesAdded.Add(fileInfo);
            }

            return new CodeGeneratorResult(filesAdded.ToArray(), filesDeleted.ToArray(), _writeDirectory);
        }

        private string GetDescription(string setting, CTree[] trees)
        {
            return (trees.FirstOrDefault(tree =>
                    tree is CDefine d &&
                    d.Name.Equals($"{setting}_STRING", StringComparison.InvariantCultureIgnoreCase)
                ) as CDefine)
                ?.ResolveValue() as string;
        }

        // ReSharper disable once TooManyDeclarations
        private Dictionary<string, object> GetPossibleValues(string setting, CTree[] trees, out bool isCustom)
        {
            isCustom = false;
            var possibleValues = trees
                .FirstOrDefault(tree =>
                    tree is CEnum e &&
                    e.Name.Equals($"EValues_{setting}", StringComparison.InvariantCultureIgnoreCase)
                )
                ?.SubTrees
                .OfType<CEnumValue>()
                .Where(value => !value.Name.EndsWith("_NUM_VALUES", StringComparison.InvariantCultureIgnoreCase))
                .ToDictionary(value => value.Name, value => value.ResolveValue());

            if (possibleValues == null || possibleValues.Count == 0)
            {
                isCustom = true;
                possibleValues = trees
                    .Where(tree =>
                        tree is CDefine d &&
                        d.Name.StartsWith(setting, StringComparison.InvariantCultureIgnoreCase)
                    )
                    .OfType<CDefine>()
                    .Where(value =>
                        !value.Name.EndsWith("_NUM_VALUES", StringComparison.InvariantCultureIgnoreCase) &&
                        !value.Name.EndsWith("_STRING", StringComparison.InvariantCultureIgnoreCase)
                    )
                    .ToDictionary(define => define.Name, define => define.ResolveValue());
            }

            return possibleValues;
        }
    }
}