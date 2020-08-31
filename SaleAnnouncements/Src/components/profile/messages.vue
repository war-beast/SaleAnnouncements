<template>
	<div class="container-fluid">
		<div class="row">
			<div class="col-sm-4">
				<div class="d-flex flex-column">
					<div class="border-bottom mb-2 pb-2" v-for="title in messageTitles">
						<span class="text-black-50"><a href="#" v-on:click="load(title.id)">{{title.name}}</a></span><br />
						<span class="text-muted">{{title.date}}</span>
					</div>
				</div>
			</div>

			<div class="col-sm-8 d-flex flex-column">
				<h5 class="h5">Переписка по объявлению <b>"{{messageThread.name}}"</b></h5>
				<div class="message-list">
					<div v-for="message in messageThread.messages">
						<div class="alert alert-primary w-75 mb-2 ml-auto" role="alert" v-if="message.isMyMessage">
							<div>{{message.name}}, <span class="text-muted">{{message.date}}</span></div>
							<div>{{message.message}}</div>
						</div>
						<div class="alert alert-secondary w-75 mb-2 mr-auto" role="alert" v-else>
							<div>{{message.name}}, <span class="text-muted">{{message.date}}</span></div>
							<div>{{message.message}}</div>
						</div>
					</div>
				</div>

				<div class="form-group">
					<label for="Message">Текст сообщения</label>
					<textarea id="Message" name="Message" class="form-control"></textarea>
					<span class="text-danger"></span>
					<span class="text-success"></span>
				</div>
				<div class="d-flex justify-content-end">
					<button type="button" class="btn btn-primary ml-auto">Отправить</button>
				</div>
			</div>
		</div>
	</div>
</template>

<script src="./messages.ts"></script>