using System.Threading.Tasks;

namespace Pasamonte.Speech.Core
{
    public interface ISpeech
    {
        Task SpeakAsync(string texto);
        void RegistrarTexto(string texto, string path);
        bool Enabled { get; set; }
    }
}
