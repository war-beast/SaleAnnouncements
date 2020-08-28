<template>
	<div class="mt-4">
		<div class="flex-row mb-4">
			<strong>Установленные платные статусы: </strong>
			<div v-if="offerExistingStatuses.length > 0">
				<span class="badge badge-success mx-1" v-for="status in offerExistingStatuses">{{status.name}}</span>
			</div>
			<span v-else>
				нет
			</span>
		</div>

		<statusSelectorComponent></statusSelectorComponent>

		<div class="d-flex justify-content-between align-items-center">
			<div>
				<span v-if="successMessage === ''">&nbsp</span>
				<div v-else class="alert alert-success" role="alert">
					{{successMessage}}
				</div>
				<div v-show="addingError !== ''" class="alert alert-danger" role="alert">
					{{addingError}}
				</div>
			</div>
			<img src="/img/loader.gif" v-if="addingInProcess" />
			<button v-on:click="submit" class="btn btn-primary" type="button" role="button" v-else v-bind:disabled="addingError !== ''">
				<span>Сохранить</span>
			</button>
		</div>
	</div>
</template>

<script lang="ts" src="./addStatus.ts"></script>