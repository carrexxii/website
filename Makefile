SRC_DIR    = ./
CLIENT_DIR = ./client

all: js fable
	dotnet run

.PHONY: watch
watch: clean js
	dotnet fable watch $(CLIENT_DIR) &
	dotnet watch run

.PHONY: js
js:
	npm run babel
	cp ./node_modules/react/umd/react.production.min.js $(CLIENT_DIR)/lib/react.js
	cp ./node_modules/react-dom/umd/react-dom.production.min.js $(CLIENT_DIR)/lib/react-dom.js

.PHONY: fable
fable:
	dotnet fable $(CLIENT_DIR)

.PHONY: install
install:
	npm install
	dotnet restore $(SRC_DIR)
	dotnet restore $(CLIENT_DIR)

.PHONY: clean
clean:
	@rm -f $(CLIENT_DIR)/*.js

.PHONY: remove
remove: clean
	@rm -rf ./*_modules
	@rm -rf $(SRC_DIR)/*_modules
	@rm -rf $(SRC_DIR)/obj/ $(CLIENT_DIR)/obj/
	@rm -rf $(SRC_DIR)/bin/ $(CLIENT_DIR)/bin/
