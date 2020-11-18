namespace BioEngine.BRC.Core.Entities.Blocks
{
    [Entity("cutblock","Кат")]
    public class CutBlock : ContentBlock<CutBlockData>
    {
        public override string ToString()
        {
            return "";
        }
    }

    public class CutBlockData : ContentBlockData
    {
        public string ButtonText { get; set; } = "Читать дальше";
    }
}
