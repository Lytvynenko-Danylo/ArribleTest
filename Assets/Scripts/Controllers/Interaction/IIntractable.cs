
namespace ArribleTest.Core
{
    public interface IIntractable
    {
        bool State { get; }
        string GetMessage();
        void ChangeState();
    }
}

