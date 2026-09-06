// ==========================================
// 1. SELECCIÓN DE ELEMENTOS DEL DOM
// ==========================================
const btnTema = document.getElementById('theme-toggle');
const formHabito = document.getElementById('form-habito');
const inputHabito = document.getElementById('input-habito');
const listaHabitosContenedor = document.getElementById('lista-habitos');
const barraProgreso = document.querySelector('.barra-progreso');
const textoPorcentaje = document.getElementById('porcentaje');
const fechaActualEl = document.getElementById('fecha-actual');

// ==========================================
// 2. ESTADO DE LA APLICACIÓN Y ESTRUCTURA
// ==========================================
let habitos = JSON.parse(localStorage.getItem('misHabitos')) || [];

// Días de la semana para el historial visual
const DIAS = ['L', 'M', 'X', 'J', 'V', 'S', 'D'];

// Mostrar la fecha actual en el encabezado
if (fechaActualEl) {
    const hoy = new Date();
    const opciones = { weekday: 'long', day: 'numeric', month: 'short' };
    fechaActualEl.textContent = hoy.toLocaleDateString('es-ES', opciones);
}

// ==========================================
// 3. LÓGICA DE TEMA (CLARO / OSCURO)
// ==========================================
function inicializarTema() {
    const temaGuardado = localStorage.getItem('tema') || 'dark';
    document.documentElement.setAttribute('data-theme', temaGuardado);
    if (btnTema) btnTema.textContent = temaGuardado === 'dark' ? '☀️' : '🌙';
}

if (btnTema) {
    btnTema.addEventListener('click', () => {
        const temaActual = document.documentElement.getAttribute('data-theme');
        const nuevoTema = temaActual === 'dark' ? 'light' : 'dark';

        document.documentElement.setAttribute('data-theme', nuevoTema);
        localStorage.setItem('tema', nuevoTema);
        btnTema.textContent = nuevoTema === 'dark' ? '☀️' : '🌙';
    });
}

// ==========================================
// 4. FUNCIONES PRINCIPALES DE HÁBITOS
// ==========================================

// Guardar lista en localStorage
function guardarEnLocalStorage() {
    localStorage.setItem('misHabitos', JSON.stringify(habitos));
}

// Renderizar la lista completa en el HTML
function renderizarHabitos() {
    listaHabitosContenedor.innerHTML = '';

    if (habitos.length === 0) {
        listaHabitosContenedor.innerHTML = `
      <p style="text-align: center; color: var(--text-muted); padding: 20px 0;">
        ¡No tienes hábitos registrados aún! Añade uno arriba. 🚀
      </p>
    `;
        actualizarProgreso();
        return;
    }

    habitos.forEach((habito) => {
        const tarjeta = document.createElement('article');
        tarjeta.className = `tarjeta-habito ${habito.completadoHoy ? 'completado' : ''}`;

        // Generar botones para los 7 días de la semana
        const diasHTML = DIAS.map((dia, index) => {
            const estaCompletado = habito.historialSemana[index] ? 'completado' : '';
            return `<span class="dia ${estaCompletado}">${dia}</span>`;
        }).join('');

        tarjeta.innerHTML = `
      <div class="habito-principal">
        <input 
          type="checkbox" 
          class="check-habito" 
          ${habito.completadoHoy ? 'checked' : ''} 
          onchange="toggleHabito(${habito.id})"
        >
        <span class="nombre-habito">${escaparHTML(habito.nombre)}</span>
        <span class="racha-habito">🔥 ${habito.racha} ${habito.racha === 1 ? 'día' : 'días'}</span>
        <button class="btn-eliminar" onclick="eliminarHabito(${habito.id})">&times;</button>
      </div>
      <div class="dias-semana">
        ${diasHTML}
      </div>
    `;

        listaHabitosContenedor.appendChild(tarjeta);
    });

    actualizarProgreso();
}

// Agregar un nuevo hábito
formHabito.addEventListener('submit', (e) => {
    e.preventDefault();
    const nombre = inputHabito.value.trim();

    if (!nombre) return;

    // Obtener índice del día actual en la semana (0: Lunes ... 6: Domingo)
    const diaIndex = (new Date().getDay() + 6) % 7;

    const nuevoHabito = {
        id: Date.now(),
        nombre: nombre,
        racha: 0,
        completadoHoy: false,
        historialSemana: [false, false, false, false, false, false, false]
    };

    habitos.push(nuevoHabito);
    guardarEnLocalStorage();
    renderizarHabitos();

    inputHabito.value = '';
    inputHabito.focus();
});

// Marcar / Desmarcar hábito como completado
window.toggleHabito = function (id) {
    const habito = habitos.find(h => h.id === id);
    if (!habito) return;

    const diaIndex = (new Date().getDay() + 6) % 7;

    habito.completadoHoy = !habito.completadoHoy;
    habito.historialSemana[diaIndex] = habito.completadoHoy;

    // Actualizar racha
    if (habito.completadoHoy) {
        habito.racha += 1;
    } else {
        habito.racha = Math.max(0, habito.racha - 1);
    }

    guardarEnLocalStorage();
    renderizarHabitos();
};

// Eliminar un hábito por ID
window.eliminarHabito = function (id) {
    habitos = habitos.filter(h => h.id !== id);
    guardarEnLocalStorage();
    renderizarHabitos();
};

// Actualizar barra y porcentaje de progreso general
function actualizarProgreso() {
    if (habitos.length === 0) {
        if (barraProgreso) barraProgreso.style.width = '0%';
        if (textoPorcentaje) textoPorcentaje.textContent = '0%';
        return;
    }

    const completados = habitos.filter(h => h.completadoHoy).length;
    const porcentaje = Math.round((completados / habitos.length) * 100);

    if (barraProgreso) barraProgreso.style.width = `${porcentaje}%`;
    if (textoPorcentaje) textoPorcentaje.textContent = `${porcentaje}%`;
}

// Función de seguridad contra ataques XSS al renderizar texto
function escaparHTML(str) {
    return str.replace(/[&<>'"]/g,
        tag => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', "'": '&#39;', '"': '&quot;' }[tag] || tag)
    );
}

// ==========================================
// 5. INICIALIZACIÓN
// ==========================================
inicializarTema();
renderizarHabitos();