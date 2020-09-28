<template>
<div>
  <loader v-if="loading" height="200"></loader>

  <!-- Lessons List -->
  <ul v-if="!loading" class="list-group">
    <li class="list-group-item text-right">
      <button @click="order" type="button" class="btn btn-success">
        Ordenar
      </button>
    </li>
    <!-- Listado de las lecciones creadas -->
    <li v-for="item in lessons" class="list-group-item">
        <div class="row">
          <div class="col-xs-2">
            <input v-model="item.Order" min="1" max="100" type="number" class="form-control text-center">  
          </div>
          <div class="col-xs-10">
            <div class="input-group">
                <input type="text" class="form-control" :value="item.Name" readonly>
                <span class="input-group-btn">
                    <button @click="remove(item.Id)" class="btn btn-danger" type="button" title="Eliminar">
                        <i class="fa fa-trash"></i>
                    </button>
                    <button @click="get(item.Id)" type="button" data-toggle="modal" data-target="#lesson-edit" class="btn btn-default" title="Editar">
                        <i class="fa fa-edit"></i>
                    </button>
                </span>
            </div>          
          </div>
        </div>
    </li>

    <!-- Nuevas lecciones -->
    <li class="list-group-item">
        <div v-if="newEntry.Error.length > 0" class="alert alert-danger">{{ newEntry.Error }}</div>
        <div class="input-group">
            <input v-model="newEntry.Name" type="text" class="form-control" placeholder="Nueva lección">
            <span class="input-group-btn">
                <button @click="insert" class="btn btn-default" type="button" title="Registrar">
                    <i class="fa fa-plus"></i>
                </button>
            </span>
        </div>
    </li>
  </ul>

  <!-- Modal -->
  <div class="modal fade" id="lesson-edit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
      <div class="modal-dialog modal-lg" role="document">
          <div class="modal-content">
              <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                  <h4 class="modal-title" id="myModalLabel">Lección #1</h4>
              </div>
              <div class="modal-body">
                <div v-if="entry.Error.length > 0" class="alert alert-danger">{{ entry.Error }}</div>
                <loader v-if="loadingEdit" height="200"></loader>
                <div v-if="!loadingEdit">
                  <div class="form-group">
                      <label>Nombre <span class="text-danger">*</span></label>
                      <input v-model="entry.Name" type="text" name="Name" class="form-control" value="Lección #1" />
                  </div>

                  <!-- Un pequeño hack para resolver esto -->
                  <div class="form-group">
                      <label>Contenido <span class="required">*</span></label>
                      <textarea id="wysiwyg" name="Content" class="form-control">{{ entry.Content }}</textarea>
                      <input id="wysiwygHidden" type="hidden" v-model="entry.Content" />
                  </div>

                  <div class="form-group">
                      <label>Video <small>[Opcional]</small></label>
                      <input v-model="entry.Video" type="text" name="Video" class="form-control" />
                      <small>Ingrese el código de su video</small>
                  </div>

                  <div class="text-right">
                      <button @click="update" type="button" class="btn btn-default">
                          Guardar
                      </button>
                  </div>
                </div>
              </div>
          </div>
      </div>
  </div>
</div>
</template>

<script>
import loader from './global.loader.vue'

export default {
  name: 'instructorlesson',
  components: {
    loader
  },
  props: {
    id: {
      type: Number,
      requide: true
    }
  },
  data() {
    return {
      loading: false,
      loadingEdit: false,
      newEntry: {
        Name: '',
        Error: ''
      },
      entry: {
        Id: 0,
        Name: '',
        Content: '',
        Video: '',
        Order: 0,
        Error: ''
      },
      lessons: []
    }
  },
  mounted() {
    this.all();
  },
  updated() {
    // Desde aqui podemos ejecutar plugins de jQuery
    var self = this;

    $('#wysiwyg').trumbowyg();

    // Pequeño hack para setear el valor manualmente
    $('#wysiwyg').on('tbwblur', function() {
      self.entry.Content = $(this).val();
    });
  },
  computed: {
    
  },
  methods: {
    all() {
      let self = this;
      self.loading = true;

      $.post('/instructor/GetAllLessons', {
        courseId: self.id
      }, function(r) {
        self.lessons = r;
        self.loading = false;
      }, 'json')
    },
    get(id) {
      var self = this;
      self.loadingEdit = true;

      $.post('/instructor/GetLesson', {
        id: id
      }, function(r) {
        self.entry.Id = r.Id;
        self.entry.Name = r.Name;
        self.entry.Content = r.Content;
        self.entry.Video = r.Video;
        self.entry.Error = '';

        self.loadingEdit = false;
      }, 'json')
    },
    update() {
      let self = this;
      self.loadingEdit = true;

      $.post('/instructor/updateLesson', self.entry, function(r) {
        self.loadingEdit = false;

        if(!r.Response) {
          // Si hay error mostramos mensaje
          self.entry.Error = r.Message;
        } else {
          self.entry.Error = '';
          self.all();
        }
      }, 'json')
    },
    insert() {
      let self = this;
      self.loading = true;

      $.post('/instructor/insertlesson', {
        courseId: self.id,
        Name: self.newEntry.Name
      }, function(r) {
        self.loading = false;

        if(!r.Response) {
          // Si hay error mostramos mensaje
          self.newEntry.Error = r.Message;
        } else {
          // En caso de éxito limpiamos todo
          self.newEntry.Name = '';
          self.newEntry.Error = '';

          self.all();
        }
      }, 'json')
    },
    remove(id) {
      if(!confirm('Esta seguro de realizar esta acción')) {
        return;
      }

      let self = this;
      self.loading = true;

      $.post('/instructor/deleteLesson', {
        id: id
      }, function(r) {
        self.loading = false;
        self.all();
      }, 'json')
    },
    order() {
      let self = this;
      self.loading = true;

      $.post('/instructor/orderLesson', {
        model: self.lessons
      }, function(r) {
        self.loading = false;
        self.all();
      }, 'json')
    }
  }
}
</script>