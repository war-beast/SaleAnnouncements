<template>
	<div v-bind:class="{ 'modal-open': isInfoVisible }" class="vue-modals">
		<div class="modal fade bd-example-modal-lg" v-bind:class="{ 'show': isInfoVisible }" tabindex="-1" role="dialog" aria-labelledby="photoModalCenterTitle" aria-hidden="true">
			<div class="modal-dialog modal-dialog-centered modal-lg" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<h5 class="modal-title" id="exampleModalLongTitle">Фото</h5>
						<button type="button" class="close" data-dismiss="modal" aria-label="Close" v-on:click="hideModal">
							<span aria-hidden="true">&times;</span>
						</button>
					</div>
					<div class="modal-body">
						<img v-bind:src="'data:image/jpeg;base64,' + imageBytes" />
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-secondary" data-dismiss="modal" v-on:click="hideModal">Закрыть</button>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script lang="ts" src="./photoModal.ts"></script>