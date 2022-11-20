function inputHandler(masks, max, event) {
	var c = event.target;
	var v = c.value.replace(/\D/g, '');
	var m = c.value.length > max ? 1 : 0;
	VMasker(c).unMask();
	VMasker(c).maskPattern(masks[m]);
	c.value = VMasker.toPattern(v, masks[m]);
}

function telMask(tel) {
	var telMask = ['(99) 9999-99999', '(99) 99999-9999'];
	VMasker(tel).maskPattern(telMask[0]);
	tel.addEventListener('input', inputHandler.bind(undefined, telMask, 14), false);
}

function cpfMask(cpf) {
	var cpfMask = ['999.999.999-99'];
	VMasker(cpf).maskPattern(cpfMask[0]);
	cpf.addEventListener('input', inputHandler.bind(undefined, cpfMask, 14), false);
}

