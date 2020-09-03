<template>
	<div v-bind:class="{ 'modal-open': isInfoVisible }" class="vue-modals">
		<!-- Message modal -->
		<div class="modal fade" v-bind:class="{ 'show': isInfoVisible }" tabindex="-1" role="dialog" aria-labelledby="messageModalLabel" aria-hidden="true">
			<div class="modal-dialog" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<h5 class="modal-title" id="exampleModalLabel">Новое сообщение</h5>
						<button type="button" class="close" data-dismiss="modal" aria-label="Close" v-on:click="hideModal">
							<span aria-hidden="true">&times;</span>
						</button>
					</div>
					<div class="modal-body">
						<div class="form-group">
							<label for="Message">Текст сообщения</label>
							<textarea class="form-control" v-model="message"></textarea>
							<span class="text-danger" v-show="submittingError !== ''">{{submittingError}}</span>
							<span class="text-success" v-show="submittingSuccess !== ''">{{submittingSuccess}}</span>
						</div>
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-secondary" data-dismiss="modal" v-on:click="hideModal">Отмена</button>
						<button type="button" class="btn btn-primary" v-on:click="sendMessage" v-bind:disabled="sendingProcess === true">Отправить</button>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script lang="ts" src="./messageModal.ts"></script>