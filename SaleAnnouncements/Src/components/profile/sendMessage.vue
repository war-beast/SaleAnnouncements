<template>
	<section>
		<div class="form-group">
			<label for="Message">Текст сообщения</label>
			<textarea class="form-control" v-model="message"></textarea>
			<span class="text-danger"></span>
			<span class="text-success"></span>
		</div>
		<div class="d-flex justify-content-between align-items-center flex-column">
			<div>
				<div class="alert alert-danger" role="alert" v-if="sendError !== ''">
					{{sendError}}
				</div>
			</div>
			<button type="button" class="btn btn-primary ml-auto" v-on:click="send">Отправить</button>
		</div>
	</section>
</template>

<script src="./sendMessage.ts"></script>