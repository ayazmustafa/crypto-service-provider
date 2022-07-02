using CryptoServiceProvider;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;

namespace crypto_service_provider;

internal class Program
{
    static void Main(string[] args)
    {
        // var personNames = File.ReadAllLines(@"..\..\..\PersonNames.txt");
        // var hashAlgorithmMD5 = MD5.Create();
        // var hashAlgorithm2SHA512 = SHA512.Create();
        //
        // var hastBytesMD5 = hashAlgorithmMD5.ComputeHash(Encoding.UTF8.GetBytes(personNames.First()));
        // Console.WriteLine(GetHashValue(hastBytesMD5));
        //
        // var hastBytesAllMD5 =
        //     hashAlgorithmMD5.ComputeHash(Encoding.UTF8.GetBytes(File.ReadAllText(@"..\..\..\PersonNames.txt")));
        // Console.WriteLine(GetHashValue(hastBytesAllMD5));
        //
        //
        // var hastBytesSHA1 = hashAlgorithm2SHA512.ComputeHash(Encoding.UTF8.GetBytes(personNames.First()));
        // Console.WriteLine(GetHashValue(hastBytesSHA1));
        //
        // var hastBytesAllSHA1 =
        //     hashAlgorithm2SHA512.ComputeHash(Encoding.UTF8.GetBytes(File.ReadAllText(@"..\..\..\PersonNames.txt")));
        // Console.WriteLine(GetHashValue(hastBytesAllSHA1));

        var personNames = File.ReadAllLines(@"..\..\..\PersonNames.txt");
        var hashAlgorithm2SHA512 = SHA512.Create();

        var tablesDictionary = new OneToManyDictionary<string, string>();

        foreach (var personName in personNames)
        {
            // var hashCode = personName.GetHashCode();
            // var machineId =Math.Abs(hashCode % 5);
            // tablesDictionary.Add(machineId.ToString(), personName);
            var hashBytes = hashAlgorithm2SHA512.ComputeHash(Encoding.UTF8.GetBytes(personName));
            var value = BitConverter.ToInt32(hashBytes);
            var machineId = Math.Abs(value % 5);
            tablesDictionary.Add(machineId.ToString(), personName);
        }

        foreach (var machine in tablesDictionary.OrderBy(x => x.Key))
        {
            Console.WriteLine($"Machine: {machine.Key}, Count: {tablesDictionary[machine.Key].Count}");
        }

        foreach (var personName in personNames)
        {
            var hashBytes = hashAlgorithm2SHA512.ComputeHash(Encoding.UTF8.GetBytes(personName));
            var value = BitConverter.ToInt32(hashBytes);
            var machineId = Math.Abs(value % 5);
        }
    }

    private static string GetHashValue(byte[] hashBytes)
    {
        var sb = new StringBuilder();
        foreach (var hashByte in hashBytes)
        {
            sb.Append(hashByte.ToString("x2"));
        }
        return sb.ToString();
    }
}