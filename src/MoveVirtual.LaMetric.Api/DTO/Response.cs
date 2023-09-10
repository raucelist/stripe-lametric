namespace MoveVirtual.LaMetric.Api.DTO;

public class Response
{
    public ICollection<Frame> Frames { get; set; }

    public Response()
    {
        Frames = new List<Frame>();
    }

    public void AddFrame(string text, string icon = "i34")
    {
        Frame frame = new()
        {
            Text = text,
            Icon = icon 
        };

        Frames.Add(frame);
    }

    public void AddErrorFrame(string error)
    {
        AddFrame(error, "i142");
    }
}