-- MySQL Workbench Synchronization
-- Generated: 2022-11-23 05:01
-- Model: New Model
-- Version: 1.0
-- Project: Name of the project
-- Author: bestl

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

ALTER SCHEMA `agendeme`  DEFAULT CHARACTER SET utf8  DEFAULT COLLATE utf8_general_ci ;

ALTER TABLE `agendeme`.`Cidadao` 
DROP FOREIGN KEY `fk_Cidadao_OrgaoPublico1`,
DROP FOREIGN KEY `fk_Cidadao_Prefeitura1`;

ALTER TABLE `agendeme`.`Agendamento` 
DROP FOREIGN KEY `fk_Agendamento_Agendamento1`,
DROP FOREIGN KEY `fk_Agendamento_AgendamentoDia1`;

ALTER TABLE `agendeme`.`OrgaoPublico` 
DROP FOREIGN KEY `fk_OrgaoPublico_Prefeitura1`;

ALTER TABLE `agendeme`.`AgendaDoServico` 
DROP FOREIGN KEY `fk_Agenda_Cidadao1`;

ALTER TABLE `agendeme`.`ServicoPublico` 
DROP FOREIGN KEY `fk_ServicoPublico_Area1`,
DROP FOREIGN KEY `fk_ServicoPublico_OrgaoPublico1`;

ALTER TABLE `agendeme`.`AreaDeServico` 
DROP FOREIGN KEY `fk_Area_Prefeitura1`;

ALTER TABLE `agendeme`.`AtendenteOrgaoPublico` 
DROP FOREIGN KEY `fk_CidadaoOrgaoPublico_Cidadao1`,
DROP FOREIGN KEY `fk_CidadaoOrgaoPublico_OrgaoPublico1`;

ALTER TABLE `agendeme`.`CargoProfissionalPrefeitura` 
DROP FOREIGN KEY `fk_CargoProfissionalPrefeitura_Cargo1`,
DROP FOREIGN KEY `fk_CargoProfissionalPrefeitura_Cidadao1`,
DROP FOREIGN KEY `fk_CargoProfissionalPrefeitura_Prefeitura1`;

ALTER TABLE `agendeme`.`DiaAgendamento` 
DROP FOREIGN KEY `fk_AgendamentoDia_ServicoPublico1`;

ALTER TABLE `agendeme`.`Cidadao` 
CHARACTER SET = utf8 , COLLATE = utf8_general_ci ,
CHANGE COLUMN `tipoCidadao` `tipoCidadao` ENUM('Administrador', 'Atendente', 'gestorOrgao', 'gestorPrefeitura', 'Profissional', 'Cidadao') NOT NULL DEFAULT 'Cidadao' ;

ALTER TABLE `agendeme`.`Agendamento` 
CHARACTER SET = utf8 , COLLATE = utf8_general_ci ;

ALTER TABLE `agendeme`.`Prefeitura` 
CHARACTER SET = utf8 , COLLATE = utf8_general_ci ;

ALTER TABLE `agendeme`.`OrgaoPublico` 
CHARACTER SET = utf8 , COLLATE = utf8_general_ci ;

ALTER TABLE `agendeme`.`AgendaDoServico` 
CHARACTER SET = utf8 , COLLATE = utf8_general_ci ;

ALTER TABLE `agendeme`.`ServicoPublico` 
CHARACTER SET = utf8 , COLLATE = utf8_general_ci ;

ALTER TABLE `agendeme`.`AreaDeServico` 
CHARACTER SET = utf8 , COLLATE = utf8_general_ci ;

ALTER TABLE `agendeme`.`Cargo` 
CHARACTER SET = utf8 , COLLATE = utf8_general_ci ;

ALTER TABLE `agendeme`.`AtendenteOrgaoPublico` 
CHARACTER SET = utf8 , COLLATE = utf8_general_ci ;

ALTER TABLE `agendeme`.`CargoProfissionalPrefeitura` 
CHARACTER SET = utf8 , COLLATE = utf8_general_ci ;

ALTER TABLE `agendeme`.`DiaAgendamento` 
CHARACTER SET = utf8 , COLLATE = utf8_general_ci ;

CREATE TABLE IF NOT EXISTS `agendeme`.`__efmigrationshistory` (
  `MigrationId` VARCHAR(150) CHARACTER SET 'utf8mb3' NOT NULL,
  `ProductVersion` VARCHAR(32) CHARACTER SET 'utf8mb3' NOT NULL,
  PRIMARY KEY (`MigrationId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;

ALTER TABLE `agendeme`.`Cidadao` 
ADD CONSTRAINT `fk_Cidadao_OrgaoPublico1`
  FOREIGN KEY (`idOrgaoPublico`)
  REFERENCES `agendeme`.`OrgaoPublico` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `fk_Cidadao_Prefeitura1`
  FOREIGN KEY (`idPrefeitura`)
  REFERENCES `agendeme`.`Prefeitura` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `agendeme`.`Agendamento` 
DROP FOREIGN KEY `fk_Agendamento_Cidadao1`,
DROP FOREIGN KEY `fk_Agendamento_Cidadao2`;

ALTER TABLE `agendeme`.`Agendamento` ADD CONSTRAINT `fk_Agendamento_Cidadao1`
  FOREIGN KEY (`idCidadao`)
  REFERENCES `agendeme`.`Cidadao` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `fk_Agendamento_Cidadao2`
  FOREIGN KEY (`idAtendente`)
  REFERENCES `agendeme`.`Cidadao` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `fk_Agendamento_Agendamento1`
  FOREIGN KEY (`idRetorno`)
  REFERENCES `agendeme`.`Agendamento` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `fk_Agendamento_AgendamentoDia1`
  FOREIGN KEY (`idDiaAgendamento`)
  REFERENCES `agendeme`.`DiaAgendamento` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `agendeme`.`OrgaoPublico` 
ADD CONSTRAINT `fk_OrgaoPublico_Prefeitura1`
  FOREIGN KEY (`idPrefeitura`)
  REFERENCES `agendeme`.`Prefeitura` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `agendeme`.`AgendaDoServico` 
DROP FOREIGN KEY `fk_Agenda_ServicoPublico1`;

ALTER TABLE `agendeme`.`AgendaDoServico` ADD CONSTRAINT `fk_Agenda_ServicoPublico1`
  FOREIGN KEY (`idServicoPublico`)
  REFERENCES `agendeme`.`ServicoPublico` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `fk_Agenda_Cidadao1`
  FOREIGN KEY (`idProfissional`)
  REFERENCES `agendeme`.`Cidadao` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `agendeme`.`ServicoPublico` 
ADD CONSTRAINT `fk_ServicoPublico_Area1`
  FOREIGN KEY (`idArea`)
  REFERENCES `agendeme`.`AreaDeServico` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `fk_ServicoPublico_OrgaoPublico1`
  FOREIGN KEY (`idOrgaoPublico`)
  REFERENCES `agendeme`.`OrgaoPublico` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `agendeme`.`AreaDeServico` 
ADD CONSTRAINT `fk_Area_Prefeitura1`
  FOREIGN KEY (`idPrefeitura`)
  REFERENCES `agendeme`.`Prefeitura` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `agendeme`.`AtendenteOrgaoPublico` 
ADD CONSTRAINT `fk_CidadaoOrgaoPublico_Cidadao1`
  FOREIGN KEY (`idAtendente`)
  REFERENCES `agendeme`.`Cidadao` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `fk_CidadaoOrgaoPublico_OrgaoPublico1`
  FOREIGN KEY (`idOrgaoPublico`)
  REFERENCES `agendeme`.`OrgaoPublico` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `agendeme`.`CargoProfissionalPrefeitura` 
ADD CONSTRAINT `fk_CargoProfissionalPrefeitura_Cargo1`
  FOREIGN KEY (`idCargo`)
  REFERENCES `agendeme`.`Cargo` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `fk_CargoProfissionalPrefeitura_Cidadao1`
  FOREIGN KEY (`idProfissional`)
  REFERENCES `agendeme`.`Cidadao` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `fk_CargoProfissionalPrefeitura_Prefeitura1`
  FOREIGN KEY (`idPrefeitura`)
  REFERENCES `agendeme`.`Prefeitura` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `agendeme`.`DiaAgendamento` 
ADD CONSTRAINT `fk_AgendamentoDia_ServicoPublico1`
  FOREIGN KEY (`idServicoPublico`)
  REFERENCES `agendeme`.`ServicoPublico` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;


INSERT INTO CARGO(nome,descricao) VALUES('Clínico Geral','Clínico Geral');

INSERT INTO CIDADAO(nome, cpf, dataNascimento, sus, telefone, email,
				    cep, estado, cidade, bairro, rua, numeroCasa,
                    sexo, tipoCidadao, complemento)
			VALUES('José da Silva', '999.999.999-09', '19981009',
           '888 9999 7777 6666','999998888', 'garelaxxx@yahool.com',
           '49530-000', 'SE', 'Ribeirópolis', 'Centro', 'Rua Manoel',
           '69','M','Profissional', 'Qualquer Complemento');
           
INSERT INTO CIDADAO(nome, cpf, dataNascimento, sus, telefone, email,
				    cep, estado, cidade, bairro, rua, numeroCasa,
                    sexo, complemento)
			VALUES('José', '999.999.929-09', '19981009',
           '888 9999 7772 6666','999998888', 'garelaxxxs@yahool.com',
           '49530-000', 'SE', 'Ribeirópolis', 'Centro', 'Rua Manoel',
           '69','M', 'Qualquer Complemento');

INSERT INTO CIDADAO(nome, cpf, dataNascimento, sus, telefone, email,
				    cep, estado, cidade, bairro, rua, numeroCasa,
                    sexo, tipoCidadao, complemento)
			VALUES('Julia Almeida', '888.999.999-09', '20001009',
           '888 5999 7777 6666','799998888', 'garelaxx2x@yahool.com',
           '49530-000', 'SE', 'Ribeirópolis', 'Centro', 'Rua Juju',
           '65','F','Cidadao', 'Qualquer Complemento');




INSERT INTO PREFEITURA(id,nome,cnpj,estado,cidade,bairro,cep,rua,numero,icone)
						value(null,'Prefeitura de Ribeirópolis',
							'897892344',
                            'SE',
                            'Ribeirópolis',
                            'Centro',
                            '2344444',
                            'Rua Teste',
                            '122',
                            'Qualquercoisaaqui');

INSERT INTO cargoprofissionalprefeitura VALUES(1,1,1);


INSERT INTO AREADESERVICO(ID,NOME, IDPREFEITURA, ICONE)
			VALUES(null,'Saúde',1,'fas fa-plus');

INSERT INTO AREADESERVICO(ID,NOME, IDPREFEITURA, ICONE)
			VALUES(null,'Transporte',1,'fas fa-bus');

INSERT INTO AREADESERVICO(ID,NOME, IDPREFEITURA, ICONE)
			VALUES(null,'Cultura',1,'fas fa-theater-masks');

INSERT INTO AREADESERVICO(ID,NOME, IDPREFEITURA, ICONE)
			VALUES(null,'Agricultura',1,'fas fa-tractor');
            
INSERT INTO AREADESERVICO(ID,NOME, IDPREFEITURA, ICONE)
			VALUES(null,'Esporte',1,'fas fa-futbol');

INSERT INTO AREADESERVICO(ID,NOME, IDPREFEITURA, ICONE)
			VALUES(null,'Negócio',1,'fas fa-briefcase');

INSERT INTO AREADESERVICO(ID,NOME, IDPREFEITURA, ICONE)
			VALUES(null,'Social',1,'fas fa-user-friends');


INSERT INTO ORGAOPUBLICO(id,nome,BAIRRO, RUA, NUMERO,
						COMPLEMENTO, CEP, horaAbre,
                        horaFecha, idPrefeitura)
			VALUES(null,'Clínica Dona Mininha', 'Centro',
			'Rua da Clínica Dona Mininha', 244,
            'Próximo ao Simas Turbo', '49530-000', '08:00',
            '18:00',1);
            
INSERT INTO ORGAOPUBLICO(id,nome,BAIRRO, RUA, NUMERO,
						COMPLEMENTO, CEP, horaAbre,
                        horaFecha, idPrefeitura)
			VALUES(null,'Clínica Lá Lá', 'Centro',
			'Rua da Clínica Lá Lá', 36,
            'Próximo ao México Lindo', '49530-000', '07:00',
            '12:00',1);

INSERT INTO ORGAOPUBLICO(id,nome,BAIRRO, RUA, NUMERO,
						COMPLEMENTO, CEP, horaAbre,
                        horaFecha, idPrefeitura)
			VALUES(null,'Clínica Dona Mininho', 'Centro',
			'Rua da Clínica Dona Mininho', 24,
            'Próximo ao Seu Cuca é Eu?', '49530-000', '08:00',
            '13:00',1);

INSERT INTO ORGAOPUBLICO(id,nome,BAIRRO, RUA, NUMERO,
						COMPLEMENTO, CEP, horaAbre,
                        horaFecha, idPrefeitura)
			VALUES(null,'Academia de Saúde', 'Centro',
			'Rua da Academia', 24,
            'Próximo ao bar do Jadeu', '49530-000', '08:00',
            '13:00',1);

INSERT INTO ORGAOPUBLICO(id,nome,BAIRRO, RUA, NUMERO,
						COMPLEMENTO, CEP, horaAbre,
                        horaFecha, idPrefeitura)
			VALUES(null,'Secretaria de Agricultura', 'Centro',
			'Rua da Academia', 24,
            'Próximo ao Tilambucano', '49530-000', '08:00',
            '13:00',1);

INSERT INTO ORGAOPUBLICO(id,nome,BAIRRO, RUA, NUMERO,
						COMPLEMENTO, CEP, horaAbre,
                        horaFecha, idPrefeitura)
			VALUES(null,'Secretaria de Transporte', 'Centro',
			'Rua da Academia', 24,
            'Próximo ao Arizona', '49530-000', '08:00',
            '13:00',1);

INSERT INTO servicopublico(ID,NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Clínico Geral',1,1,'fas fa-user-md');

INSERT INTO servicopublico(ID,NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Clínico Geral',1,2,'fas fa-user-md');

INSERT INTO servicopublico(ID, NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Clínico Geral',1,3,'fas fa-user-md');
            
INSERT INTO servicopublico(ID, NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Psicólogo',1,3,'fas fa-brain');

INSERT INTO servicopublico(ID, NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Psicólogo',1,1,'fas fa-brain');

INSERT INTO servicopublico(ID, NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Cardiologista',1,2,'fas fa-heartbeat');

INSERT INTO servicopublico(ID, NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Cardiologista',1,1,'fas fa-heartbeat');

INSERT INTO servicopublico(ID, NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Nutricionista',1,1,'fas fa-apple-alt');

INSERT INTO servicopublico(ID, NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Odontologista',1,1,'fas fa-tooth');

INSERT INTO servicopublico(ID, NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Odontologista',1,2,'fas fa-tooth');

INSERT INTO servicopublico(ID, NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Odontologista',1,3,'fas fa-tooth');

INSERT INTO servicopublico(ID, NOME, IDAREA, IDORGAOPUBLICO, icone)
			VALUES(null,'Treino Terceira Idade',7,4,'fas fa-blind');

SET lc_time_names = 'pt_BR';
INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-24',DAYNAME('2022-10-24'),'08:00','12:00',
								 10,2,1);
                                 
INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-24',DAYNAME('2022-10-24'),'13:00','17:00',
								 10,2,1);
                                 
INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-25',DAYNAME('2022-10-25'),'08:00','12:00',
								 10,2,1);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-25',DAYNAME('2022-10-25'),'13:00','17:00',
								 10,2,1);
                                 
INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-26',DAYNAME('2022-10-26'),'08:00','12:00',
								 10,2,1);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-26',DAYNAME('2022-10-26'),'13:00','17:00',
								 10,2,1);
                                 
INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-27',DAYNAME('2022-10-27'),'08:00','12:00',
								 10,2,1);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-27',DAYNAME('2022-10-27'),'13:00','17:00',
								 10,2,1);
                                 
INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-28',DAYNAME('2022-10-28'),'08:00','12:00',
								 10,2,1);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-28',DAYNAME('2022-10-28'),'13:00','17:00',
								 10,2,1);



INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-24',DAYNAME('2022-10-24'),'08:00','12:00',
								 10,2,2);
                                 
INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-24',DAYNAME('2022-10-24'),'13:00','17:00',
								 10,2,3);
                                 
INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-25',DAYNAME('2022-10-25'),'08:00','12:00',
								 10,2,2);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-25',DAYNAME('2022-10-25'),'13:00','17:00',
								 10,2,3);
                                 
INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-26',DAYNAME('2022-10-26'),'08:00','12:00',
								 10,2,2);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-26',DAYNAME('2022-10-26'),'13:00','17:00',
								 10,2,3);
                                 
INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-27',DAYNAME('2022-10-27'),'08:00','12:00',
								 10,2,2);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-27',DAYNAME('2022-10-27'),'13:00','17:00',
								 10,2,3);
                                 
INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-28',DAYNAME('2022-10-28'),'08:00','12:00',
								 10,2,2);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-28',DAYNAME('2022-10-28'),'13:00','17:00',
								 10,2,3);


INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-28',DAYNAME('2022-10-28'),'07:00','09:00',
								 10,2,12);


INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-28',DAYNAME('2022-10-28'),'15:00','17:00',
								 10,2,12);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-24',DAYNAME('2022-10-28'),'08:00','12:00',
								 10,2,9);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-28',DAYNAME('2022-10-28'),'08:00','12:00',
								 10,2,9);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-25',DAYNAME('2022-10-28'),'08:00','12:00',
								 10,2,10);

INSERT INTO diaagendamento(ID,DATA,diaSemana,horarioInicio,horarioFim,
						   vagasAtendimento, vagasRetorno, idServicoPublico)
                           VALUES(NULL,'2022-10-27',DAYNAME('2022-10-28'),'08:00','12:00',
								 10,2,10);

INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Segunda','08:00','12:00',10,2,1,1);

INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Segunda','13:00','17:00',10,2,1,1);
            
	
INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Segunda','08:00','12:00',10,2,1,1);

INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Segunda','13:00','17:00',10,2,1,1);

INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Terça','08:00','12:00',10,2,1,1);

INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Terça','13:00','17:00',10,2,1,1);

INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Quarta','08:00','12:00',10,2,1,1);


INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Quarta','13:00','17:00',10,2,1,1);

INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Quinta','08:00','12:00',10,2,1,1);

INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Quinta','13:00','17:00',10,2,1,1);

INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Sexta','08:00','12:00',10,2,1,1);

INSERT INTO AGENDADOSERVICO(id, diaSemana, 
							horarioInicio, 
                            horarioFim, 
                            vagasAtendimento, 
                            vagasRetorno, 
                            idServicoPublico,
                            idProfissional)
			values(null,'Sexta','13:00','17:00',10,2,1,1);