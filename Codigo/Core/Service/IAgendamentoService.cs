﻿using Core.DTO;

namespace Core.Service
{
    public interface IAgendamentoService
    {

        Task<int> Create(Agendamento agendamento);
        void Edit(Agendamento agendamento);
        void Delete(int idAgendamento);
        Agendamento Get(int idAgendamento);
        AgendamentoPage GetAllByUser(int id, int page);
        AgendamentoDTO GetDados(int idAgendamento);

    }
}
