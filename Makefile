SRC_DIR    = ./
CLIENT_DIR = ./client

all: js
	dotnet run

.PHONY: watch
watch: js
	npm run babel-watch &
	dotnet watch run

.PHONY: js
js:
	npm run babel
	mv ./$(CLIENT_DIR)/*.js ./$(CLIENT_DIR)/js/
	cp ./node_modules/react/umd/react.production.min.js $(CLIENT_DIR)/js/react.js
	cp ./node_modules/react-dom/umd/react-dom.production.min.js $(CLIENT_DIR)/js/react-dom.js

.PHONY: restore
restore:
	dotnet restore $(SRC_DIR) &
	dotnet restore $(CLIENT_DIR) &
	npm install

.PHONY: remove
remove:
	@rm -rf $(SRC_DIR)/*_modules
	@rm -rf $(SRC_DIR)/obj/ $(CLIENT_DIR)/obj/
	@rm -rf $(SRC_DIR)/bin/ $(CLIENT_DIR)/bin/
