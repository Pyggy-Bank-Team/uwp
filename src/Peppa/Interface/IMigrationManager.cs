using System;

namespace Peppa.Interface
{
    public interface IMigrationManager : IDisposable
    {
        void Migrate();
    }
}