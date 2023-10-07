using GithubBackupTool.Infrastructure.Interfaces;

namespace GithubBackupTool.Infrastructure
{
    public class MockEncryptionKeyProvider : IEncryptionKeyProvider
    {
        public byte[] Key => new byte[] { 112, 164, 13, 154, 183, 3, 164, 158, 199, 50, 186, 41, 128, 192, 46, 198, 152, 162, 149, 54, 164, 255, 85, 26, 123, 180, 230, 47, 48, 51, 39, 76 };
        public byte[] IV => new byte[] { 166, 245, 180, 27, 145, 36, 44, 169, 116, 216, 127, 211, 253, 100, 139, 12 };
    };
}

