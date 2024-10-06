namespace Attri.Runtime
{
    public interface IElement
    {
        public int Size();
        public float[] AsFloat();
        public int[] AsInt();
        public string[] AsString();
        public object[] AsObject();
        public ushort[] HalfValues();
        public byte[] AsByte();
        public uint[] AsUint();
    }
}
