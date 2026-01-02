<script setup>
import { ref, onMounted } from 'vue'

// 1. Estado para los contactos (Mock Data inicial)
const contacts = ref([
  { id: 1, firstName: 'Ejemplo', lastName: 'Inicial', email: 'demo@vue.com', phone: '123-456-7890' },
  { id: 2, firstName: 'Otro', lastName: 'Cliente', email: 'cliente@test.com', phone: '987-654-3210' }
])

// 2. Funciones Placeholder (Las conectaremos a la API en el siguiente paso)
const handleDelete = (id) => {
  if(confirm('¿Estás seguro de eliminar este contacto?')) {
    // Aquí irá la llamada DELETE a la API
    contacts.value = contacts.value.filter(c => c.id !== id)
    console.log('Eliminado localmente:', id)
  }
}

const handleEdit = (contact) => {
  console.log('Editar:', contact)
  alert(`Aquí abriremos el formulario para editar a ${contact.firstName}`)
}

const handleCreate = () => {
  alert('Aquí abriremos el formulario de creación')
}
</script>

<template>
  <div class="space-y-6">
    <div class="md:flex md:items-center md:justify-between">
      <div class="flex-1 min-w-0">
        <h2 class="text-2xl font-bold leading-7 text-gray-900 sm:text-3xl sm:truncate">
          Lista de Contactos
        </h2>
        <p class="mt-1 text-sm text-gray-500">
          Gestiona tu base de clientes desde aquí.
        </p>
      </div>
      <div class="mt-4 flex md:mt-0 md:ml-4">
        <button 
          @click="handleCreate"
          class="ml-3 inline-flex items-center px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 transition-colors"
        >
          <svg class="-ml-1 mr-2 h-5 w-5" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 4v16m8-8H4" />
          </svg>
          Nuevo Contacto
        </button>
      </div>
    </div>

    <div class="flex flex-col">
      <div class="-my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
        <div class="py-2 align-middle inline-block min-w-full sm:px-6 lg:px-8">
          <div class="shadow overflow-hidden border-b border-gray-200 sm:rounded-lg">
            <table class="min-w-full divide-y divide-gray-200 bg-white">
              <thead class="bg-gray-50">
                <tr>
                  <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Nombre Completo
                  </th>
                  <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Contacto
                  </th>
                  <th scope="col" class="relative px-6 py-3">
                    <span class="sr-only">Acciones</span>
                  </th>
                </tr>
              </thead>
              <tbody class="divide-y divide-gray-200">
                <tr v-for="contact in contacts" :key="contact.id" class="hover:bg-gray-50 transition-colors">
                  <td class="px-6 py-4 whitespace-nowrap">
                    <div class="flex items-center">
                      <div class="flex-shrink-0 h-10 w-10">
                        <div class="h-10 w-10 rounded-full bg-blue-100 flex items-center justify-center text-blue-600 font-bold">
                          {{ contact.firstName.charAt(0) }}
                        </div>
                      </div>
                      <div class="ml-4">
                        <div class="text-sm font-medium text-gray-900">
                          {{ contact.firstName }} {{ contact.lastName }}
                        </div>
                        <div class="text-sm text-gray-500">
                          ID: {{ contact.id }}
                        </div>
                      </div>
                    </div>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap">
                    <div class="text-sm text-gray-900">{{ contact.email }}</div>
                    <div class="text-sm text-gray-500">{{ contact.phone }}</div>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                    <button @click="handleEdit(contact)" class="text-indigo-600 hover:text-indigo-900 mr-4 font-semibold">
                      Editar
                    </button>
                    <button @click="handleDelete(contact.id)" class="text-red-600 hover:text-red-900 font-semibold">
                      Eliminar
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>