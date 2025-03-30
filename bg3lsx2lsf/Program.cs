using System.Globalization;
using LSLib.LS;
using LSLib.LS.Enums;

// needed for correct xml reading because of floating point/comma separators
Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;


const ResourceFormat inputFormat = ResourceFormat.LSX;
const ResourceFormat outputFormat = ResourceFormat.LSF;
string lsxInputDirectory = args[0];
string lsfOutputDirectory = args[1];

var loadParams = new ResourceLoadParameters { ByteSwapGuids = true };

var conversionParams = new ResourceConversionParameters
{
    ByteSwapGuids = true,
    Compression = CompressionMethod.None,
    CompressionLevel = LSCompressionLevel.Default,
    LSF = LSFVersion.VerBG3Patch3,
    LSX = LSXVersion.V4,
    MetadataFormat = null,
    PAKVersion = PackageVersion.V18,
    PrettyPrint = true
};

var utils = new ResourceUtils();
utils.errorDelegate += ResourceError;

utils.ConvertResources(lsxInputDirectory, lsfOutputDirectory, inputFormat, outputFormat, loadParams, conversionParams);
return;


void ResourceError(string path, Exception e)
{
    Console.WriteLine($"Error converting {path}: {e.Message}");
}