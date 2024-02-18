# Nyktos

Hola, en este documento se encuentran las pautas para evitar terrorismo sobre el repositorio

## Workflow de trabajo

¿Quieres ponerte a currar? Esta es la forma adecuada de hacerlo

1. Vete al github projects y busca una historia con la etiqueta de la milestone actual y el sprint actual. Utiliza las prioridades para elegir tu historia
    - Si no quedan historias en este sprint puedes moverlas al sprint actual, siempre que no haya nada para review
2. Antes de asignarte la tarea pregunta si todo el mundo está de acuerdo, comprueba que la tarea no dependa de ninguna otra y leete los comentarios que pueda tener. En caso de que lo tengas claro asignate la tarea
3. Mueve tu historia a in-progress y en el Inicio pon la fecha actual
4. Cuando hayas terminado el trabajo crea una escena en el apartado Demos del proyecto, que tenga un nombre diferenciador, y pasa la historia al estado de review. Pon un comentario en la issue de la historia en el que indicas en que escena está la demo
5. Si tu tarea ha pasado el proceso de review esta será cerrada por tu revisor

## Workflow de revisión

¿Quieres revisar algo? Aquí te explico como

1. Vete al apartado de revisión del projects y busca alguna historia a revisar sin ningún revisor asignado (no confundir con el campo asignees)
2. Habla con el encargado de la historia, comenta con el resto de compañeros que vas a hacer la revisión 
3. Comprueba los comentarios de la issue de la historia para ver en que escena estará la demo y ponte a hacer la review
4. Una vez revisado el trabajo coméntale al encargado de la historia tu review y escribe un comentario sobre la review en la issue de la historia. En caso de que la historia esté incompleta o tenga fallos devuelvela a in-progress. En caso de que todo vaya bien pásala a done y marca el campo "Fin" con la fecha actual

Por favor, sed cordiales y educados en el proceso de review. Mucho amor y brillitos

## Jerarquías y convenciones

### Tipos de escenas

Las escenas van a estar divididas en 3 tipos  

- **Develop**: área de trabajo de cada miembro del equipo, exclusivas únicamente de dicho miembro
- **Demos**: escenas para hacer pruebas de concepto de las características implementadas en las historias de usuario y estas puedan ser revisadas
- **Production**: escenas finales para los hitos y para el juego final

### Funciones y comentarios

Intentad segmentar los scripts en funciones locales, de tal forma que actúen como caja negra (tened cuidado de no sobreencapsular)  

Comentad funciones, métodos, areas de código con una breve descripción de lo que hace, no especifiquéis como funciona en profundidad  

Comentad también vuestro nombre en lo que hayáis programado (con sentido común, sin pasarse) para poder saber quien ha hecho que, y así preguntar directamente a la persona encargada de esa parte sin que todo sea un putísimo caos