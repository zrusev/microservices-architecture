pipeline {
  agent any
  stages {
    stage('Verify Branch') {
      steps {
        echo "$GIT_BRANCH"
      }
    }
    stage('Run Unit Tests') {
      steps {
        sh(script: """
          cd Server
          dotnet test
          cd ..
        """)
      }
    }
    stage('Docker Build For Development') {
      when { branch 'development' }
      steps {
        sh(script: 'docker-compose -f docker-compose.yml -f development.yml build')
        sh(script: 'docker images -a')
      }
    }
    stage('Docker Build For Production') {
      when { branch 'master' }
      steps {
        sh(script: 'docker-compose -f docker-compose.yml -f production.yml build')
        sh(script: 'docker images -a')
      }
    }
    stage('Run Test Application') {
      steps {
        sh(script: 'docker-compose up -d')
      }
    }
    stage('Run Integration Tests') {
      steps {
        script {
          sleep 30
          try {
            sh "bash ./Tests/Startup.sh"
            sh "bash ./Tests/Register.sh"
            sh "bash ./Tests/Login.sh"
            sh "bash ./Tests/TopProducts.sh"
            sh "bash ./Tests/Statistics.sh"
          } catch (Exception e) {
            currentBuild.result = 'UNSTABLE'
          }
        }
      }
    }
    stage('Stop Test Application') {
      steps {
        sh(script: 'docker-compose down')
        // sh(script: 'docker volumes prune -f')
      }
      post {
        success {
          echo "Build successfull! You should deploy! :)"
        }
        failure {
          script {
              emailext (
                subject: "UNSUCCESSFUL: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]'",
                body: """<p>UNSUCCESSFUL: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]':</p>
                  <p>Check console output at <a href='${env.BUILD_URL}'>${env.JOB_NAME} [${env.BUILD_NUMBER}]</a></p>""",
                recipientProviders: [
                  [$class: 'DevelopersRecipientProvider']
                ],
                replyTo: '$DEFAULT_REPLYTO',
                to: '$DEFAULT_RECIPIENTS'
              )
          }
        }
      }
    }
    stage('Push Images') {
      when {
        expression { env.BRANCH_NAME ==~ /(master|development)/ }
      }
      steps {
        script {
          def images = [
            'zlatkorusev/microservices-architecture-identity-service',
            'zlatkorusev/microservices-architecture-customers-service',
            'zlatkorusev/microservices-architecture-statistics-service',
            'zlatkorusev/microservices-architecture-orders-service',
            'zlatkorusev/microservices-architecture-admin-service',
            'zlatkorusev/microservices-architecture-customers-gateway-service',
            'zlatkorusev/microservices-architecture-notifications-service',
            'zlatkorusev/microservices-architecture-monitoring-service',
            'zlatkorusev/microservices-architecture-client']

          if(env.BRANCH_NAME == 'development') {
            images.pop();
            images.push('zlatkorusev/microservices-architecture-client-development');
          }

          for (int i = 0; i < images.size(); ++i) {
            docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
              def image = docker.image(images[i])
              image.push("1.0.${env.BUILD_ID}")
              image.push('latest')
            }
          }
        }
      }
    }
    stage('Deploy Development') {
      when { branch 'development' }
      steps {
        withKubeConfig([credentialsId: 'DevelopmentServer', serverUrl: 'https://0B31E5854BC45F8F19DCAA146E072E31.gr7.eu-south-1.eks.amazonaws.com']) {
		       sh(script: 'kubectl apply -f ./.k8s/.environment/development.yml')
		       sh(script: 'kubectl apply -f ./.k8s/databases')
		       sh(script: 'kubectl apply -f ./.k8s/event-bus')
		       sh(script: 'kubectl apply -f ./.k8s/web-services')
           sh(script: 'kubectl apply -f ./.k8s/clients')
           sh(script: 'kubectl set image deployments/user-client user-client=zlatkorusev/microservices-architecture-client-development:latest')
        }
      }
    }
    stage('Deploy Production') {
      when { branch 'master' }
      steps {
        contentReplace(
            configs: [
                fileContentReplaceConfig(
                    configs: [
                        fileContentReplaceItemConfig(
                            search: 'latest',
                            replace: "1.0.${env.BUILD_ID}",
                            matchCount: 0)
                        ],
                    fileEncoding: 'UTF-8',
                    filePath: './.k8s/clients/*.yml')
                ])
        contentReplace(
            configs: [
                fileContentReplaceConfig(
                    configs: [
                        fileContentReplaceItemConfig(
                            search: 'latest',
                            replace: "1.0.${env.BUILD_ID}",
                            matchCount: 0)
                        ],
                    fileEncoding: 'UTF-8',
                    filePath: './.k8s/web-services/*.yml')
                ])
        script {
          def USER_INPUT = input(
                    message: 'Would you like to deploy on production?',
                    parameters: [
                            [$class: 'ChoiceParameterDefinition',
                             choices: ['No','Yes'].join('\n'),
                             name: 'input',
                             description: 'Menu - select box option']
                    ])

          if( "${USER_INPUT}" == "Yes"){
            withKubeConfig([credentialsId: 'ProductionServer', serverUrl: 'https://C4DCEBD4B5FA9251AAAEDDFFBB01F043.sk1.eu-south-1.eks.amazonaws.com']) {
              sh(script: 'kubectl apply -f ./.k8s/databases')
              sh(script: 'kubectl apply -f ./.k8s/event-bus')
              sh(script: 'kubectl apply -f ./.k8s/web-services')
              sh(script: 'kubectl apply -f ./.k8s/clients')
            }
          }
        }
      }
    }
    stage('Send Success Email') {
      steps {
        script {
            emailext (
              subject: "SUCCESSFUL: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]'",
              body: """<p>SUCCESSFUL: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]':</p>
                <p>Check console output at <a href='${env.BUILD_URL}'>${env.JOB_NAME} [${env.BUILD_NUMBER}]</a></p>""",
              recipientProviders: [
                [$class: 'DevelopersRecipientProvider']
              ],
              replyTo: '$DEFAULT_REPLYTO',
              to: '$DEFAULT_RECIPIENTS'
            )
        }
      }
    }
  }
}
