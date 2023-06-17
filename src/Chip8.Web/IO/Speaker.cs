using Chip8.Model.IO;
using System.Threading.Tasks.Dataflow;

namespace Chip8.Web.IO;

public class Speaker : ISpeaker
{
    private readonly ITargetBlock<bool> setBuzzer;

    public Speaker(ITargetBlock<bool> setBuzzer)
    {
        this.setBuzzer = setBuzzer;
    }

    public void SetBuzzer(bool on)
    {
        setBuzzer.Post(on);
    }
}
