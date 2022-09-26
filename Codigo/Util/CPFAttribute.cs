using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Util
{
	/// <summary>
	/// Validação customizada para CPF
	/// </summary>
	public class CPFAttribute : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{
			if (value == null || string.IsNullOrEmpty(value.ToString()))
				return true;
			var valueNoEspecial = Methods.RemoveSpecialsCaracts((string)value);
			bool valido = Methods.ValidarCpf(valueNoEspecial.ToString());
			return valido;
		}

		public string GetErrorMessage() =>
			$"CPF Inválido";
	}
}
