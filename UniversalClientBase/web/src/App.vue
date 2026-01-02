<script setup>
import ContactList from './components/ContactList.vue'
</script>

<template>
  <div class="min-h-screen bg-gray-100 font-sans text-gray-900">
    <nav class="bg-white shadow-sm border-b border-gray-200">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex justify-between h-16">
          <div class="flex items-center">
            <div class="flex-shrink-0 flex items-center gap-2">
              <div class="w-8 h-8 bg-blue-600 rounded-lg flex items-center justify-center">
                <span class="text-white font-bold text-xl">U</span>
              </div>
              <h1 class="text-xl font-bold text-gray-800 tracking-tight">
                Universal Client Base
              </h1>
            </div>
          </div>
        </div>
      </div>
    </nav>

    <main class="max-w-7xl mx-auto py-10 px-4 sm:px-6 lg:px-8">
      <ContactList />
    </main>
  </div>
</template>