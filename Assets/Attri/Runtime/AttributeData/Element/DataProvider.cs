namespace Attri.Runtime
{
    public interface IDataProvider
    {
        public int Dimension();
        public float[] AsFloat();
        public int[] AsInt();
        public string[] AsString();
        public object[] AsObject();
        public ushort[] HalfValues();
        public byte[] AsByte();
        public uint[] AsUint();
    }
}
