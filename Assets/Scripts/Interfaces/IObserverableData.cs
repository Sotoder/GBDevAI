public interface IObserverableData
{
    void Attach(IEnemy enemy);
    void Detach(IEnemy enemy);
}