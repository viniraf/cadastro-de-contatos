﻿using CadastroContato.Enums;
using CadastroContato.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContato.Models {
    public class UsuarioModel {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuario")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do usuario")]
        public String Login { get; set; }

        [Required(ErrorMessage = "Digite a senha do usuario")]
        public String Senha { get; set; }

        [Required(ErrorMessage = "Digite o email do usuario")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Informe o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public virtual List<ContatoModel> Contatos { get; set; }

        public bool SenhaValida(string senha) {
            return Senha == senha.GerarHash();
        }


        public void SetPasswordHash() {
            Senha = Senha.GerarHash();
        }

        public void SetNewPassword(string novaSenha) {

            Senha = novaSenha.GerarHash();
        }
    }
}
