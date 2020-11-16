namespace Gameplay
{
    public abstract class Killable : QuestItem
    {
        private OnKilled onKilled = delegate {  };
        public delegate void OnKilled();

        public void AddEventKilled(OnKilled cb)
        {
            onKilled += cb;
        }
        
        protected void TriggerKilledCallback()
        {
            onKilled.Invoke();
            onKilled = delegate {  };
        }
    }
}
