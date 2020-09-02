<template>
	<a class="mx-1 my-1 pointer" v-on:click="zoom">
		<img v-bind:src="'data:image/jpeg;base64,' + imageBytes" />
	</a>
</template>

<script lang="ts" src="./photoItemComponent.ts"></script>