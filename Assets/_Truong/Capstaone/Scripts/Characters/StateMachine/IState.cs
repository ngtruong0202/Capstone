
namespace Truong
{
    public interface IState
    {
        public void Enter();

        public void Exit();

        public void HandleInput();

        public void UpDate();

        public void PhysicUpdate();
        public void OnAnimationEnterEvent();
        public void OnAnimationExitEvent();
        public void OnAnimationTransitionEvent();

    }

}
