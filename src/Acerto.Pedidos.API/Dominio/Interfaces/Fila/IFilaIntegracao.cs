namespace Acerto.Pedidos.API.Dominio.Interfaces.Fila
{
    public interface IFilaIntegracao
    {
        void EnviarMensagem<T>(T message);
    }
}