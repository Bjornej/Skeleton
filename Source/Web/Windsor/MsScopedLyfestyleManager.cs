using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;

namespace Web.Windsor
{

    public class MsScopedLifestyleManager : ScopedLifestyleManager
    {
        public MsScopedLifestyleManager()
            : base(new MsScopedAccesor())
        {

        }

        public override object Resolve(CreationContext context, IReleasePolicy releasePolicy)
        {
            if (MsLifetimeScope.Current == null)
            {
                //Act as transient!
                var burden = CreateInstance(context, false);
                Track(burden, releasePolicy);
                return burden.Instance;
            }

            return base.Resolve(context, releasePolicy);
        }
    }
}
