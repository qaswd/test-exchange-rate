pipeline {
    agent any

    environment {
        DOTNET_VERSION = '3.1'
        IMAGE_NAME = 'currency-gateway-api'
        IMAGE_TAG = 'latest'
        PROJECT_PATH = 'ExchangeRate/ExchangeRate'
        PUBLISH_DIR = 'publish'
    }

    tools {
        dotnet "${DOTNET_VERSION}"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                dir("${PROJECT_PATH}") {
                    sh 'dotnet restore'
                }
            }
        }

        stage('Build') {
            steps {
                dir("${PROJECT_PATH}") {
                    sh 'dotnet build --configuration Release --no-restore'
                }
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test --configuration Release --no-build'
            }
        }

        stage('Publish') {
            steps {
                dir("${PROJECT_PATH}") {
                    sh "dotnet publish -c Release -o ${PUBLISH_DIR} --no-build"
                }
            }
        }

        stage('Docker Build') {
            steps {
                script {
                    docker.build("${IMAGE_NAME}:${IMAGE_TAG}", "${PROJECT_PATH}")
                }
            }
        }

        stage('Docker Push (Optional)') {
            when {
                expression { return env.DOCKER_USERNAME != null }
            }
            steps {
                withCredentials([usernamePassword(credentialsId: 'docker-hub', usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')]) {
                    script {
                        sh "echo $DOCKER_PASSWORD | docker login -u $DOCKER_USERNAME --password-stdin"
                        sh "docker tag ${IMAGE_NAME}:${IMAGE_TAG} ${DOCKER_USERNAME}/${IMAGE_NAME}:${IMAGE_TAG}"
                        sh "docker push ${DOCKER_USERNAME}/${IMAGE_NAME}:${IMAGE_TAG}"
                    }
                }
            }
        }
    }

    post {
        always {
            cleanWs()
        }
    }
}
