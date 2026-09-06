import pygame
import sys

# 1. Inicialización
pygame.init()
ANCHO, ALTO = 1200, 720
pantalla = pygame.display.set_mode((ANCHO, ALTO))
pygame.display.set_caption("Carrera Drag - Prototipo")
reloj = pygame.time.Clock()

# Colores
NEGRO = (0, 0, 0)
BLANCO = (255, 255, 255)
ROJO = (255, 50, 50)
VERDE = (50, 255, 50)
AZUL = (50, 50, 255)

# 2. Variables del Auto
auto_x = 50
auto_y = 400
velocidad = 0
aceleracion = 0.2
meta_x = 700

# 3. Variables del Tacómetro (Revoluciones)
rev_barra_y = 250
rev_direccion = 1
zona_perfecta_min = 120
zona_perfecta_max = 160
mensaje_feedback = ""
tiempo_feedback = 0

# Fuente de texto
fuente = pygame.font.SysFont("Arial", 24)

# Bucle Principal
ejecutando = True
juego_terminado = False

while ejecutando:
    pantalla.fill(NEGRO)
    
    # Manejo de Eventos
    for evento in pygame.event.get():
        if evento.type == pygame.QUIT:
            ejecutando = False
            
        if evento.type == pygame.KEYDOWN and not juego_terminado:
            if evento.key == pygame.K_SPACE:
                # Comprobar si el cambio de marcha fue en la zona verde
                if zona_perfecta_min <= rev_barra_y <= zona_perfecta_max:
                    velocidad += 4  # Gran impulso
                    mensaje_feedback = "¡CAMBIO PERFECTO!"
                else:
                    velocidad += 0.5  # Mal cambio, poco impulso
                    mensaje_feedback = "Mal cambio..."
                tiempo_feedback = 30  # Cuadros que dura el mensaje
                rev_barra_y = 290  # Reiniciar aguja de revoluciones

    if not juego_terminado:
        # Movimiento del coche (fricción constante + velocidad)
        velocidad = max(0, velocidad - 0.02)  
        auto_x += velocidad
        
        # Simular revoluciones subiendo constantemente
        rev_barra_y -= 4 * rev_direccion
        if rev_barra_y < 50 or rev_barra_y > 290:
            rev_direccion *= -1  # Rebota si no haces el cambio
            
        # Condición de Meta
        if auto_x >= meta_x:
            juego_terminado = True
            mensaje_feedback = "¡LLEGASTE A LA META!"

    # --- DIBUJAR ELEMENTOS ---
    
    # Línea de Meta
    pygame.draw.rect(pantalla, BLANCO, (meta_x, 350, 10, 100))
    
    # Pista (Carretera)
    pygame.draw.rect(pantalla, (50, 50, 50), (0, 450, ANCHO, 50))
    
    # Auto del Jugador (Caja Azul)
    pygame.draw.rect(pantalla, AZUL, (int(auto_x), auto_y, 60, 30))
    
    # interfaz del Tacómetro (Barra de cambios)
    pygame.draw.rect(pantalla, ROJO, (700, 50, 30, 250)) # Barra total (Fondo malo)
    pygame.draw.rect(pantalla, VERDE, (700, 300 - zona_perfecta_max, 30, zona_perfecta_max - zona_perfecta_min)) # Zona Verde
    pygame.draw.circle(pantalla, BLANCO, (715, 300 - rev_barra_y), 8) # Aguja/Indicador
    
    # Textos en pantalla
    txt_velocidad = fuente.render(f"Velocidad: {int(velocidad * 10)} km/h", True, BLANCO)
    pantalla.blit(txt_velocidad, (20, 20))
    
    txt_instrucciones = fuente.render("Presiona ESPACIO en la zona VERDE para meter marcha", True, BLANCO)
    pantalla.blit(txt_instrucciones, (20, 60))

    if tiempo_feedback > 0 or juego_terminado:
        txt_feedback = fuente.render(mensaje_feedback, True, VERDE if "PERFECTO" in mensaje_feedback or "META" in mensaje_feedback else ROJO)
        pantalla.blit(txt_feedback, (300, 200))
        tiempo_feedback -= 1

    pygame.display.flip()
    reloj.tick(60)

pygame.quit()
sys.exit()
