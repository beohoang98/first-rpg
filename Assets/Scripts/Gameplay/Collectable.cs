namespace Gameplay
{
    public abstract class Collectable : QuestItem
    {
        private OnCollect onCollect = delegate {  };
        public delegate void OnCollect();

        public void AddEventCollected(OnCollect cb)
        {
            onCollect += cb;
        }
        protected void TriggerCollectCallback()
        {
            onCollect.Invoke();
            onCollect = delegate {  };
        }
    }
}
