namespace Framework
{
    public interface IReferencePoolObject 
    {
        void OnCreate();
        void OnRecycle();
        void OnDispose();
    }   
}
